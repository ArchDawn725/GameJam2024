using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlayer : MonoBehaviour
{

    Vector3 velocity;
    float rotation = 0;
    public float speed = 5f, maxSpeed = 5f;
    public float rotationSpeed = 200f;
    public AudioClip owSound;
    public GameObject deadPrefab;
    

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
        transform.position += (velocity) * Time.fixedDeltaTime;
    }

    void getInput()
    {   
        var deltaVelocity = new Vector2(0, Input.GetAxis("Vertical") * speed * Time.deltaTime);
        velocity += transform.rotation * deltaVelocity;
        velocity = Vector2.ClampMagnitude(velocity, maxSpeed);
        if (Input.GetAxis("Vertical") == 0)
            velocity *= 0.99f;
        rotation = Input.GetAxis("Horizontal");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(owSound != null)
            AudioSource.PlayClipAtPoint(owSound, transform.position);
        if(deadPrefab != null)
            Instantiate(deadPrefab, transform.position, transform.rotation);
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        gameObject.GetComponent<ScreenFlipper>().enabled = false;
        gameObject.transform.position = new Vector3(-5000,0,-20);
        yield return new WaitForSeconds(1);
        transform.position = Vector3.zero;
        velocity = Vector3.zero;
        gameObject.GetComponent<ScreenFlipper>().enabled = true;
    }

}
