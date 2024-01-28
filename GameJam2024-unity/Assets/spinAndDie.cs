using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinAndDie : MonoBehaviour
{
    public float rotationAccel = 0;
    public float rotationSpeed = 0;

    // Update is called once per frame
    void Update()
    {
        rotationSpeed += rotationAccel * Time.deltaTime;
        transform.rotation *= Quaternion.Euler(0, 0, rotationSpeed * Time.deltaTime);
        transform.localScale *= 0.99f;
        Invoke("removeMe",1.95f);
    }

    void removeMe()
    {
        Destroy(gameObject);
    }
}
