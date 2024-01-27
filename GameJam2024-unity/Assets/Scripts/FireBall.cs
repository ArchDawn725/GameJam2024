using System;
using UnityEngine;
public class FireBall : MonoBehaviour
{
    public Vector3 target;
    [SerializeField] private float speed;
    private void Start()
    {
        target = FindObjectOfType<PlayerMovement>().transform.position;
    }
    private void Update()
    {
        if (target != null)
        {
            Vector3 position = Vector3.Lerp(transform.position, target, speed);

            transform.position = position;

            if (transform.position.y <= target.y + 1f)
            {
                Explode();
            }
        }
    }
    private void Explode()
    {
        Destroy(gameObject, 1);
    }
}
