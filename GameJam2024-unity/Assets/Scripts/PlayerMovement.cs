using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    //components
    private Rigidbody2D rigidbody2D;
    private CapsuleCollider2D capsuleCollider;
    private BoxCollider2D boxCollider;
    private Animator animator;

    private Vector2 moveInput;

    //values
    [SerializeField] float movement_Speed;
    [SerializeField] float jump_Speed;
    [SerializeField] float climb_Speed;

    [SerializeField] Vector2 death_Velocity;

    private bool isDead;
    private int currentScene;
    private float starting_Gravity;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        rigidbody2D = GetComponent<Rigidbody2D>();
        starting_Gravity = rigidbody2D.gravityScale;
        animator = transform.GetChild(0).GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }
    private void Update()
    {
        if (isDead) { return; }
        Movement();
        FlipSprite();
        ClimbLadder();
        Death();
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
        animator.SetBool("Moving", IsPlayerMoving());
    }
    private void ClimbLadder()
    {
        if (isDead) { return; }
        if (!capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) { rigidbody2D.gravityScale = starting_Gravity; animator.SetBool("Climbing", false); return; }
        rigidbody2D.gravityScale = 0;
        Vector2 climbVelocity = new Vector2(rigidbody2D.velocity.x, moveInput.y * climb_Speed);
        rigidbody2D.velocity = climbVelocity;
        animator.SetBool("Climbing", IsPlayerClimbing());
    }
    private void FlipSprite()
    {
        if (IsPlayerMoving()) { transform.localScale = new Vector2(Mathf.Sign(rigidbody2D.velocity.x), 1f); }
    }
    private bool IsPlayerMoving()
    {
        return Mathf.Abs(rigidbody2D.velocity.x) > Mathf.Epsilon;
    }
    private bool IsPlayerClimbing()
    {
        return Mathf.Abs(rigidbody2D.velocity.y) > Mathf.Epsilon;
    }
    private void Death()
    {
        if (capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")) || capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Hazard")))
        {
            isDead = true;
            animator.SetTrigger("Dead");
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x + death_Velocity.x, rigidbody2D.velocity.y + death_Velocity.y);
            Invoke("RestartLevel", 1);
        }
    }
    private void RestartLevel() { SceneManager.LoadScene(currentScene); }
    private void NextLevel() { SceneManager.LoadScene(currentScene + 1); }
    private void FirstLevel() { SceneManager.LoadScene(0); }
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
