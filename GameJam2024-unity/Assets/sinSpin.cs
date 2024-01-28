using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class sinSpin : MonoBehaviour
{
    Quaternion startRotation;
    public Vector3 rotationAxis = new Vector3(0,0,1);

    // Start is called before the first frame update
    void Start()
    {
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = startRotation * Quaternion.Euler( math.sin(Time.time * 2) * rotationAxis);
    }
}
