using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveEnemiga : Enemy
{
    // Variables p�blicas para ajustar en el Inspector
    public GameObject bulletPrefab;   // Prefab de la bala que disparar�
    public Transform firePoint;       // Punto desde donde dispara
    public float fireRate = 1.0f;     // Intervalo entre disparos
    private float nextFireTime = 0.0f;

    public float horizontalSpeed = 1.0f; // Velocidad de movimiento horizontal
    private bool movingRight = true;     // Controla la direcci�n de movimiento horizontal

    // Inicializaci�n
    protected override void Start()
    {
        base.Start(); // Llama a la inicializaci�n base
        nextFireTime = Time.time + fireRate; // Inicia el contador para disparar
    }

    // Actualizaci�n
    protected override void Update()
    {
        base.Update(); // Llama a la actualizaci�n base
        Shoot();       // Dispara si es el momento adecuado
    }

    // M�todo para disparar balas
    private void Shoot()
    {
        if (Time.time >= nextFireTime)
        {
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);  // Dispara una bala
            nextFireTime = Time.time + fireRate;  // Actualiza el tiempo para el pr�ximo disparo
        }
    }

    // Sobrescribe el m�todo de movimiento para un movimiento m�s complejo
    protected override void Move()
    {
        // Movimiento de zigzag horizontal combinado con movimiento hacia abajo
        float moveX = horizontalSpeed * (movingRight ? 1 : -1) * Time.deltaTime;
        float moveY = -speed * Time.deltaTime;

        transform.Translate(new Vector3(moveX, moveY, 0));

        // Cambiar de direcci�n al llegar a los l�mites de la pantalla
        float screenLimitX = 8.0f; // Ajusta este valor seg�n el tama�o de tu pantalla
        if (transform.position.x >= screenLimitX)
        {
            movingRight = false;
        }
        else if (transform.position.x <= -screenLimitX)
        {
            movingRight = true;
        }
    }
}
