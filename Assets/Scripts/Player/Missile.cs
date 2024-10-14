using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Missile : Bullet
{
    public Vector2 direction;

    void Start()
    {
        // Inicialización si es necesario
    }

    void Update()
    {
        Movement();
    }

    public override void Movement() // Método virtual para que los hijos puedan sobrescribir
    {
        transform.Translate(direction * speed * Time.deltaTime);
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