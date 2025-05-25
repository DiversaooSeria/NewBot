using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DragDropPhase2 : MonoBehaviour
{
    public LayerMask possibleToMoveLayer;
    public float dragOffset = 0.5f;
    public static bool moving;
    private bool isDragging = false;
    private Vector2 originalPosition; 

    private Rigidbody2D rb;
    public CameraLock mainCamera;

    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        moving = false;
        // Detecta o clique do mouse sobre o personagem
        if (Input.GetMouseButtonDown(0))
        {
            mainCamera.LockCamera();
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

            if (hitCollider != null && hitCollider.gameObject == gameObject)
            {

                isDragging = true;
                originalPosition = rb.position; // Guarda a posição original
            }
        }

        // Detecta o movimento do mouse enquanto o botão está pressionado
        if (isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rb.MovePosition(mousePosition + Vector2.up * dragOffset); // Move com um pequeno deslocamento para evitar sobreposição
        }

        // Detecta quando o botão do mouse é solto
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Verifica se a posição final está em uma área válida
            if (CanMoveTo(mousePosition))
            {
                moving = true;
                rb.MovePosition(mousePosition);
                mainCamera.UnlockCamera();
            }
            else
            {
                rb.MovePosition(originalPosition); // Retorna à posição original se a posição final for inválida
            }
        }
        

        bool CanMoveTo(Vector2 position)
        {
            // Verifica colisões com a camada de lugares válidos para mover
            Collider2D hitCollider = Physics2D.OverlapCircle(position, 0.1f, possibleToMoveLayer);
            return hitCollider != null;
        }
    }
}
