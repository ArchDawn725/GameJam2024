using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlayer : MonoBehaviour
{

    Vector3 velocity;
    float rotation = 0;
    public float speed = 5f, maxSpeed = 5f;
    public float rotationSpeed = 200f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        getInput();
    }

    void FixedUpdate()
    {
        transform.rotation *= Quaternion.Euler(0, 0, rotation * rotationSpeed * Time.fixedDeltaTime);
        transform.position += (transform.rotation * (Vector3)velocity) * Time.fixedDeltaTime;
    }

    void getInput()
    {   
        var deltaVelocity = new Vector2(0, Input.GetAxis("Vertical") * speed * Time.deltaTime);
        velocity += transform.rotation * deltaVelocity;
        velocity = Vector2.ClampMagnitude(velocity, maxSpeed);
        rotation = Input.GetAxis("Horizontal");
    }
}
