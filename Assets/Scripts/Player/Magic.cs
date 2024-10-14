using System.Collections.Generic;
using UnityEngine;

public class MagicBullet : Bullet
{
    private GameObject targetEnemy; // Referencia al enemigo

    void Start()
    {
        // Inicializar el objetivo en la posición de la bala
        targetEnemy = FindClosestEnemy();
    }

    void Update()
    {
        Movement();
    }

    public override void Movement()
    {
        if (targetEnemy != null)
        {
            // Calcular la dirección hacia el enemigo
            Vector2 objetivo = (targetEnemy.transform.position - transform.position).normalized;
            transform.Translate(objetivo * speed*3 * Time.deltaTime);
        }
        else
        {
            // Si no hay enemigo, seguir en la dirección predeterminada
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
    }

    GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
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
