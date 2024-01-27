using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //components
    private Rigidbody2D rigidbody2D;
    private CapsuleCollider2D capsuleCollider;
    private BoxCollider2D boxCollider;

    private Vector2 moveInput;

    //values
    [SerializeField] float movement_Speed;
    [SerializeField] float jump_Speed;

    private bool isDead;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }
    private void Update()
    {
        if (isDead) { return; }
        Movement();
        FlipSprite();
    }
    private void OnMove(InputValue value)
    {
        if (isDead) { return; }
        moveInput = value.Get<Vector2>();
    }
    private void OnJump(InputValue value)
    {
        if (isDead) { return; }
        if (!boxCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        if (value.isPressed)
        {
            rigidbody2D.velocity += new Vector2(0, jump_Speed);
        }
    }
    private void Movement()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * movement_Speed, rigidbody2D.velocity.y);
        rigidbody2D.velocity = playerVelocity;

        //animator
    }
    private void FlipSprite()
    {
        if (PlayerMoving()) { transform.localScale = new Vector2(Mathf.Sign(rigidbody2D.velocity.x), 1f); }
    }
    private bool PlayerMoving()
    {
        return Mathf.Abs(rigidbody2D.velocity.x) > Mathf.Epsilon;
    }
}
