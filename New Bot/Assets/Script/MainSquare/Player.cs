using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rig;
    private Animator animator;
    private Vector2 playerDirection;
    private bool canMove = true;

    // Hashes dos parâmetros do Animator
    private static readonly int MoveX = Animator.StringToHash("moveX");
    private static readonly int MoveY = Animator.StringToHash("moveY");
    private static readonly int IsMoving = Animator.StringToHash("isMoving");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!canMove)
        {
            playerDirection = Vector2.zero;
            animator.SetBool(IsMoving, false);
            return;
        }

        ReadInput();
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        if (!canMove || playerDirection == Vector2.zero)
            return;

        rig.MovePosition(rig.position + playerDirection * moveSpeed * Time.fixedDeltaTime);
    }

    private void ReadInput()
    {
        playerDirection = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );
    }

    private void UpdateAnimation()
    {
        animator.SetBool(IsMoving, playerDirection != Vector2.zero);

        if (playerDirection != Vector2.zero)
        {
            animator.SetFloat(MoveX, playerDirection.x);
            animator.SetFloat(MoveY, playerDirection.y);
        }
    }

    public void EnableMovement()
    {
        canMove = true;
    }

    public void DisableMovement()
    {
        canMove = false;
        playerDirection = Vector2.zero;
        animator.SetBool(IsMoving, false);
    }
}