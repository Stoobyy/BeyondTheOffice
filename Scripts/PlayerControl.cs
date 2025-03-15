using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Animator animator;


    private Rigidbody2D rb;
    private float speedX;
    private float speedY;

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update ()
    {
        speedX = Input.GetAxisRaw("Horizontal");
        speedY = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate ()
    {
        rb.linearVelocity = new Vector2(speedX * moveSpeed, speedY * moveSpeed);
        animator.SetFloat("moveX", speedX);
        animator.SetFloat("moveY", speedY);
        bool isMoving = (speedX != 0 || speedY != 0);
        animator.SetBool("isMoving", isMoving);
    }

}
