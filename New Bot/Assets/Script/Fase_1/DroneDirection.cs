using UnityEngine;

public class DroneController : MonoBehaviour
{
    public float speed = 5f;
    private Animator animator;
    private Vector2 movement;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        MoveDrone();
        RotateDrone();
    }

    void MoveDrone()
    {
        transform.Translate(movement.normalized * speed * Time.deltaTime);
    }

    void RotateDrone()
    {
        if (movement == Vector2.zero) return;

        if (movement.y > 0)
            transform.rotation = Quaternion.Euler(0, 0, 0); // Cima
        else if (movement.y < 0)
            transform.rotation = Quaternion.Euler(0, 0, 180); // Baixo
        else if (movement.x > 0)
            transform.rotation = Quaternion.Euler(0, 0, -90); // Direita
        else if (movement.x < 0)
            transform.rotation = Quaternion.Euler(0, 0, 90); // Esquerda
    }
}
