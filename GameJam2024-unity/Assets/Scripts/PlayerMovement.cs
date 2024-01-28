using System.Collections;
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
    private const int MAX_HEALTH = 3;

    private Vector2 moveInput;

    //values
    [SerializeField] float movement_Speed;
    [SerializeField] float jump_Speed;
    [SerializeField] float climb_Speed;

    [SerializeField] Vector2 hit_Velocity;
    [SerializeField] Vector2 death_Velocity;
    [SerializeField] Vector2 level_End_Velocity;
    [SerializeField] float bounce_Pad_Velocity;
    [SerializeField] Vector2 max_Velocity;


    [SerializeField] float immunityTime;

    private bool isDead;
    private int currentScene;
    private float starting_Gravity;
    [SerializeField] private int health;
    private bool immunity;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        rigidbody2D = GetComponent<Rigidbody2D>();
        starting_Gravity = rigidbody2D.gravityScale;
        animator = transform.GetChild(0).GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        health = MAX_HEALTH;
    }
    private void Update()
    {
        if (isDead) { return; }
        Movement();
        FlipSprite();
        ClimbLadder();
        Hit();
        MaxVelocity();
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
        if (!capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))) { rigidbody2D.gravityScale = starting_Gravity; animator.SetBool("Climbing", false); return; }
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
    private void Hit()
    {
        if (!immunity)
        {
            if (capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")) || capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Hazard")))
            {
                print("Hit");
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x + hit_Velocity.x, rigidbody2D.velocity.y + hit_Velocity.y);
                health--;
                StartCoroutine(ImmunityTime());
            }
        }
        if (capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Lava"))) { health--; }
        if (health <= 0) { Death(); }

    }
    private void Death()
    {
        isDead = true;
        animator.SetTrigger("Dead");
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x + death_Velocity.x, rigidbody2D.velocity.y + death_Velocity.y);
        LivesController.Instance.player_Lives--;

        if (LivesController.Instance.player_Lives > 0) { Invoke("RestartLevel", 1); }
        else { Destroy(LivesController.Instance.gameObject); Invoke("FirstLevel", 1); }
    }
    private void RestartLevel() { SceneManager.LoadScene(currentScene); }
    private void NextLevel() {  SceneManager.LoadScene(currentScene + 1); }
    private void FirstLevel() { SceneManager.LoadScene(0); }
    private void Heal() { health++; if (health > MAX_HEALTH) { health = MAX_HEALTH; } }
    private void OneUp() { print("Life UP"); }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Exit")
        {
            collision.GetComponent<Animator>().SetTrigger("Trigger");
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x + level_End_Velocity.x, rigidbody2D.velocity.y + level_End_Velocity.y);
            Invoke("NextLevel", 1);
        }
        if (collision.gameObject.tag == "Meat") { Heal(); Destroy(collision.gameObject); }
        if (collision.gameObject.tag == "DinoNuggie") { OneUp(); Destroy(collision.gameObject); }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BouncePad")
        {
            collision.gameObject.GetComponent<Animator>().SetTrigger("Trigger");
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y + bounce_Pad_Velocity);
        }
    }
    private void MaxVelocity()
    {
        if(rigidbody2D.velocity.x > max_Velocity.x)
        {
            rigidbody2D.velocity = new Vector2(max_Velocity.x, rigidbody2D.velocity.y);
        }
        if (rigidbody2D.velocity.y > max_Velocity.y)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.y, max_Velocity.x);
        }
    }
    private IEnumerator ImmunityTime()
    {
        immunity = true;
        yield return new WaitForSeconds(immunityTime);
        immunity = false;
    }
}
