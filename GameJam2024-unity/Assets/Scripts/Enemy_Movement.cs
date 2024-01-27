using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    //components
    private Rigidbody2D rigidbody2D;

    //values
    [SerializeField] float movement_Speed;


    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        rigidbody2D.velocity = new Vector2(movement_Speed, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        movement_Speed = -movement_Speed;
        FlipFace();
    }
    private void FlipFace()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(rigidbody2D.velocity.x)), 1);
    }
}
