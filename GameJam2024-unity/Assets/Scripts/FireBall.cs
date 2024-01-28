using System;
using UnityEngine;
public class FireBall : MonoBehaviour
{
    public Vector3 target;
    [SerializeField] private float speed;
    Animator animator;
    [SerializeField] float spawn_Item_Chance;
    [SerializeField] GameObject[] spawnables;
    [SerializeField] AudioSource explosion;
    private bool exploded;
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
        if (exploded) { return; }
        exploded = true;
        transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        explosion.Play();
        if (UnityEngine.Random.Range(0, 100) <= spawn_Item_Chance) { SpawnItem(); }
        Invoke("Destroy", 1);
    }
    private void SpawnItem()
    {
        int value = UnityEngine.Random.Range(0, spawnables.Length);
        animator.SetTrigger("Trigger");
        Instantiate(spawnables[value], transform.position, Quaternion.identity);
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }
}
