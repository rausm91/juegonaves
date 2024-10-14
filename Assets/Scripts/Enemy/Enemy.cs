using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f;
    public int health = 1;
    public GameObject explosionPrefab;

    protected virtual void Start()
    {
        // Inicialización si es necesario
    }

    protected virtual void Update()
    {
        // Actualización común para todos los enemigos
        Move();
    }

    // Marca el método Move() como virtual para que pueda ser sobrescrito
    protected virtual void Move()
    {
        // Movimiento simple hacia abajo (puedes personalizarlo según tu juego)
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    protected void Die()
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}