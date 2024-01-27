using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidController : MonoBehaviour
{
    public int Size = 5;
    public float rotation = 10f;

    void OnEnable()
    {
        transform.localScale = Vector3.one * (Size/5.0f);
    }

    void Update()
    {
        transform.position += transform.up * Time.deltaTime * -1 * (2f/Size);
        rotation += Time.deltaTime * Random.Range(-15f, 15f);
        transform.rotation = Quaternion.Euler(0, 0, rotation );
    }

    public void OnHit()
    {
        if(Size > 1)
        {
            var astroid1 = Instantiate(gameObject, transform.position + (Vector3.right * 2), Quaternion.identity);
            var astroid2 = Instantiate(gameObject, transform.position + (Vector3.left * 2), Quaternion.identity);
            astroid1.GetComponent<AstroidController>().Size = Size - 1;
            astroid2.GetComponent<AstroidController>().Size = Size - 1;
            astroid1.GetComponent<AstroidController>().rotation = rotation+45;
            astroid2.GetComponent<AstroidController>().rotation = rotation-45;
        }
        Destroy(gameObject);
    }
    
}
