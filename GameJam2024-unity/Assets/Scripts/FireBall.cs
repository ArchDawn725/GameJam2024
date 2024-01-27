using System;
using UnityEngine;
public class FireBall : MonoBehaviour
{
    public Transform target;
    [SerializeField] private float speed;
    private void Update()
    {
        if (target != null)
        {
            Vector3 position = Vector3.Lerp(transform.position, target.position, speed);

            transform.position = position;

            if (transform.position.x <= target.position.x)
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
