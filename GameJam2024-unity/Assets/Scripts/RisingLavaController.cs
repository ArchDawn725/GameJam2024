using System.Collections;
using UnityEngine;
public class RisingLavaController : MonoBehaviour
{
    [SerializeField] private float delay;
    [SerializeField] private float riseAmount;
    private void Start()
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
