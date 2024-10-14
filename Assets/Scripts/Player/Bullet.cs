using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5.0f;

    void Start()
    {
        // Inicialización si es necesario
    }

    void Update()
    {
        Movement();
    }

    public virtual void Movement() // Método virtual para que los hijos puedan sobrescribir
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Destroy(collision.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}