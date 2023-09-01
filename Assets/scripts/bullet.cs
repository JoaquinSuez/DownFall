using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float bulletSpeed;
    private Rigidbody2D rb;
    public void StartFunc()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(0,-bulletSpeed),ForceMode2D.Impulse);
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (!other.gameObject.CompareTag("player"))
        {
            DestroyFunc(other);
        }
        if (other.gameObject.CompareTag("batEnemy"))
        {
            other.gameObject.GetComponent<batScript>().Die();
        }
    }
    private void DestroyFunc(Collider2D other)
    {
        this.gameObject.SetActive(false);
    }
}