using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float maxSize = 1, minSize = 0.1f;
    bool swinging = false;
    IEnumerator Swing()
    {
        swinging = true;
        float elapsedTime = 0f;
        while(elapsedTime < 0.5f)
        {
            elapsedTime += Time.deltaTime;
            transform.localScale = new Vector3(1, Mathf.Lerp(minSize, maxSize, elapsedTime / 0.5f), 1);
            yield return  null;
        }
        elapsedTime = 0f;
        while(elapsedTime < 0.5f)
        {
            elapsedTime += Time.deltaTime;
            transform.localScale = new Vector3(1, Mathf.Lerp(maxSize, minSize, elapsedTime / 0.5f), 1);
            yield return null;
        }
        swinging = false;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !swinging)
        {
            SwingWeapon();
        }
    }
                
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(string.Format("Collision with {0}", collision.gameObject.name));
        var hit = collision.gameObject.GetComponent<AstroidController>();
        if ( hit!= null)
        {
            hit.OnHit();
        }
    }

    public void SwingWeapon()
    {
        StartCoroutine(Swing());
    }

}
