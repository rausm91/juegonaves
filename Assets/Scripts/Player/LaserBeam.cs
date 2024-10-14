using System.Collections;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public float duration = 2f;        // Duración del rayo láser
    private LineRenderer lineRenderer;
    private BoxCollider2D boxCollider;

    void Start()
    {
        // Obtener o agregar el componente LineRenderer
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        // Configurar el LineRenderer
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.1f;  // Ancho del rayo
        lineRenderer.endWidth = 0.1f;
        lineRenderer.useWorldSpace = true;

        // Configurar el material del LineRenderer
        // Asegúrate de asignar un material en el Inspector o por código
        // lineRenderer.material = ...;

        // Obtener o agregar el componente BoxCollider2D
        boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider == null)
        {
            boxCollider = gameObject.AddComponent<BoxCollider2D>();
        }

        boxCollider.isTrigger = true;

        // Iniciar la rutina del rayo láser
        StartCoroutine(LaserRoutine());
    }

    IEnumerator LaserRoutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            UpdateLaser();
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Destruir el objeto después de la duración
        Destroy(gameObject);
    }

    void UpdateLaser()
    {
        // Establecer la posición inicial del rayo (posición del jugador)
        Vector3 startPosition = transform.position;

        // Establecer la posición final del rayo (final de la pantalla)
        Vector3 endPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1f, 0));
        endPosition.z = 0f;

        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, endPosition);

        // Actualizar el tamaño y posición del collider
        float length = Vector3.Distance(startPosition, endPosition);
        boxCollider.size = new Vector2(lineRenderer.startWidth, length);
        boxCollider.offset = new Vector2(0f, length / 2f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Destruir el enemigo al contacto
            Destroy(collision.gameObject);
        }
    }
}
