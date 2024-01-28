using System.Collections;
using UnityEngine;
public class RisingLavaController : MonoBehaviour
{
    [SerializeField] private float StartDelay = 1.5f;
    [SerializeField] private float delay;
    [SerializeField] private float riseAmount;
    private void Start()
    {
        Invoke("Delay", StartDelay);
    }
    private void Delay()
    {
        StartCoroutine(RisingLava());
    }
    private IEnumerator RisingLava()
    {
        yield return new WaitForSeconds(delay);
        transform.Translate(Vector3.up * riseAmount);
        StartCoroutine(RisingLava());
    }
}
