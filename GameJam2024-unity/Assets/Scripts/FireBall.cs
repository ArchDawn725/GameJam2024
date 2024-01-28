using System;
using UnityEngine;
public class FireBall : MonoBehaviour
{
    public Vector3 target;
    [SerializeField] private float speed;
    Animator animator;
    [SerializeField] float spawn_Item_Chance;
    private void Start()
    {
        target = FindObjectOfType<PlayerMovement>().transform.position;
        animator = GetComponent<Animator>();
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
        animator.SetTrigger("Trigger");
        transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        if (UnityEngine.Random.Range(0, 100) <= spawn_Item_Chance) { SpawnItem(); }
        Destroy(gameObject, 1);
    }
    private void SpawnItem()
    {

    }
}
