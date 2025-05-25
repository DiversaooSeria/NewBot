using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float moveSpeed;
    private Rigidbody2D rig;
    private UnityEngine.Vector2 _playerDirection;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        
    }
    void Start()
    {
         rig = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        _playerDirection = new UnityEngine.Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if(_playerDirection != Vector2.zero)
        {
            animator.SetFloat("moveX", _playerDirection.x);
            animator.SetFloat("moveY", _playerDirection.y);
            animator.SetBool("isMoving", true);
            
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
    void FixedUpdate(){
        rig.MovePosition(rig.position + _playerDirection * moveSpeed * Time.fixedDeltaTime);
    }
}