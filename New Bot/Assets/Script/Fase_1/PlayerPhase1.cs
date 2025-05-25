using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerPhase1 : MonoBehaviour
{
    private Rigidbody2D rb;

    public int movementDirection { get; set; }
    public float gridSize { get; set; }
    public bool isMoving = false;
    public float moveSpeed = 5f;

    private void Start()
    {   
        this.movementDirection = -5;
        this.gridSize = 0.5f;
        GameManager gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void MoveCharacter(int index)
    {
        if (!isMoving)
        {
            Vector2 moveVector = Vector2.zero;

            if (index == 1)
            {
                moveVector = Vector2.left * movementDirection;
            }
            else if (index == 2)
            {
                moveVector = Vector2.right * movementDirection;
            }
            else if (index == 3)
            {
                moveVector = Vector2.down * movementDirection;
            }
            else if (index == 4)
            {
                moveVector = Vector2.up * movementDirection;
            }

            if (moveVector != Vector2.zero)
            {
                StartCoroutine(MoveSmoothly(moveVector));
            }
        }
    }

    private IEnumerator MoveSmoothly(Vector2 direction)
    {
        isMoving = true;

        Vector2 startPosition = transform.position;
        Vector2 targetPosition = startPosition + direction * gridSize;

        float elapsedTime = 0f;
        float duration = gridSize / moveSpeed;

        while (elapsedTime < duration)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        isMoving = false;
    }



    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        StopAllCoroutines();
        isMoving = false;
    }
}