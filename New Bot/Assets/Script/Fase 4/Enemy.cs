using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public EnemyAttr attributes; // receber o ScriptableObject do inimigo

    private Vector2 direction, A, B, lastPosition, inDirection; // 
    public Collider2D target;
    public Animator animator;

    public static event Action<Transform> Trans; //EventoSimples 
    public UnityEvent ChangeModeEvent; //Evento Com ScriptableObject

    public bool isWaiting;
    public float waitTime;
    
    


    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
        target = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        attributes.mode = false;
        isWaiting = false;
        StartCoroutine(SetAnimations());
    }

    // Update is called once per frame
    void Update()
    {

        if (attributes.mode) // O modo do inimigo � assincro ao do player. Dessa maneira o @mode controla se esta em 'idle' ou persegui��o.
        {
            Chase(target);
        }
        if(isWaiting) // @IsWaiting s� para controlar quando entra aqui.
        {
            isWaiting = false; // S� quero que aconte�a uma vez.
            StartCoroutine // inicio da corrotina
                (
                    SequenciaCorrotina  // corrotina que garante a execu��o de corrotinas em sequencia
                        (
                            new List<IEnumerator> // lista de corrotina
                            {
                                Wait(waitTime),
                                attributes.move(transform, animator),
                                ChangeModePlayer()
                            }
                        )
                
                );
        }
        
    }

    // Escutando evento touch
    void OnEnable()
    {
        BoxEvent.touch += Chase;
    }
    private void OnDisable()
    {
        BoxEvent.touch -= Chase;
        StopAllCoroutines();
    }
    // refatorar
    public void Chase(Collider2D pos) // Fun��o para perseguir o player.
    {
        target = pos;
        A = new Vector2(transform.position.x, transform.position.y);
        B = new Vector2(target.transform.position.x, target.transform.position.y);

        // Posi��es candidatas para o inimigo parar (gpt code)
        Vector2[] stopPositions = new Vector2[]
        {
            new Vector2(B.x + 2, B.y), // � direita
            new Vector2(B.x - 2, B.y), // � esquerda
            new Vector2(B.x, B.y + 2), // acima
            new Vector2(B.x, B.y - 2)  // abaixo
        };

        // Vari�vel para armazenar a posi��o mais pr�xima
        Vector2 closestPosition = stopPositions[0];
        float closestDistance = Vector2.Distance(transform.position, stopPositions[0]);

        // Encontrar a posi��o mais pr�xima
        for (int i = 1; i < stopPositions.Length; i++)
        {
            float distance = Vector2.Distance(transform.position, stopPositions[i]);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPosition = stopPositions[i];
            }
        }
        if (transform.position.x != closestPosition.x  || transform.position.y != closestPosition.y )
        {
            attributes.mode = true;
            transform.position = Vector2.MoveTowards(A, closestPosition, attributes.speed * Time.deltaTime);
        }
        else 
        {
            inDirection = (B-A).normalized;
            attributes.inDirection = inDirection;
            Debug.Log("foi");
            attributes.mode = false;
            Trans.Invoke(this.transform); // manda o transform position para a c�mera. 
            ChangeModeEvent.Invoke(); // informa o player que o modo de jogo mudou.
            animator.SetBool("isMoving", false);
            //OnDisable();    // para o inimigo ficar parado apos a intera��o
            isWaiting = true;   // para come�ar o contador de 10 segundos
        }
    } 
    

    private IEnumerator SetAnimations() // fun��o para ficar atualizando a cada frame a anima��o do inimigo.
    {
        while (true)
        {
            if ((Vector2)transform.position != lastPosition)
            {
                // Calcula a varia��o de posi��o
                direction = ((Vector2)transform.position - lastPosition).normalized;
                animator.SetFloat("x", direction.x);
                animator.SetFloat("y", direction.y);
                animator.SetBool("isMoving", true);

                // Atualiza a �ltima posi��o
                lastPosition = transform.position;
                yield return new WaitForEndOfFrameUnit();
            }
            else
            {
                animator.SetBool("isMoving", false);
                yield return new WaitForEndOfFrameUnit();
            }
        }
    }

    private IEnumerator ChangeModePlayer()
    {
        ChangeModeEvent.Invoke();
        yield return null;
    } // Corrotina para alterar o modo do jogador via evento.

    private IEnumerator Wait(float time) // Corrotina para esperar um @time tempo, em segundos.
    {
        yield return new WaitForSeconds(time);
    }

    //Fun��o para executar uma lista de corrotinas de maneira sequencial e ordenada.
    IEnumerator SequenciaCorrotina(List<IEnumerator> coroutines)
    {
        foreach (var coroutine in coroutines)
        {
            yield return StartCoroutine(coroutine);
        }

    }



}


