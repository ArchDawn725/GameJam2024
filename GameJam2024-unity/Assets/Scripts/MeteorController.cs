using System.Collections;
using UnityEngine;
public class MeteorController : MonoBehaviour
{
    [SerializeField] private Transform Meteor;
    [SerializeField] private float min_delay;
    [SerializeField] private float max_delay;

    [SerializeField] private float spawnDistance;

    private void Start()
    {
        StartCoroutine(SpawnFireball());
    }
    private IEnumerator SpawnFireball()
    {
        yield return new WaitForSeconds(Random.Range(min_delay, max_delay));
        Vector3 playerPos = FindObjectOfType<PlayerMovement>().transform.position;
        Vector3 spawnPosition = new Vector3(playerPos.x, playerPos.y + spawnDistance, 0);
        Instantiate(Meteor, spawnPosition, Quaternion.identity);
        StartCoroutine(SpawnFireball());
    }
}
