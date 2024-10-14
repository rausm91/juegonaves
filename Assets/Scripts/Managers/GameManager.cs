using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Prefab del enemigo (asteroide)
    public GameObject enemyPrefab;
    public float spawnTime = 1.5f;      // Tiempo entre spawns de asteroides
    private float time = 0.0f;          // Tiempo transcurrido para asteroides

    // Prefab de la nave enemiga
    public GameObject naveEnemigaPrefab;
    public float naveSpawnTime = 5.0f;  // Tiempo entre spawns de naves enemigas
    private float naveTime = 0.0f;      // Tiempo transcurrido para naves enemigas

    public Player player;
    public TextMeshProUGUI liveText;
    public TextMeshProUGUI WeaponText;
    public TextMeshProUGUI TimeText;

    private float totalTime = 0.0f;

    [Header("UI")]
    public Image BulletImage;           // Imagen del arma actual
    public List<Sprite> bulletSprites;  // Lista de sprites para las armas

    [Header("Vida UI")]
    public List<Image> lifeImages;      // Lista de imágenes para las vidas (3 imágenes)

    // Update is called once per frame
    void Update()
    {
        CreateEnemy();         // Crear asteroides
        CreateNaveEnemiga();   // Crear naves enemigas
        UpdateCanvas();
        changeBulletImage(player.actualWeapon);

        totalTime += Time.deltaTime;

        // Formatear el tiempo en minutos y segundos y actualizar el texto
        TimeText.text = string.Format("{0:0}:{1:00}",
            Mathf.FloorToInt(totalTime / 60), Mathf.FloorToInt(totalTime % 60));

        UpdateLifeUI();  // Actualizar la UI de las vidas
    }

    // Método para cambiar la imagen del arma actual
    public void changeBulletImage(int index)
    {
        Debug.Log("ChangeBulletImage:" + index);
        BulletImage.sprite = bulletSprites[index];
    }

    // Método para actualizar la UI de las vidas
    public void UpdateLifeUI()
    {
        // Recorre todas las imágenes de vida y las oculta si el jugador ha perdido vidas
        for (int i = 0; i < lifeImages.Count; i++)
        {
            if (i < player.lives)
            {
                lifeImages[i].enabled = true;  // Mostrar imagen si la vida está activa
            }
            else
            {
                lifeImages[i].enabled = false; // Ocultar imagen si el jugador ha perdido esta vida
            }
        }
    }

    // Actualizar el canvas con la información del jugador
    void UpdateCanvas()
    {
        liveText.text = "Life: " + player.lives;
        WeaponText.text = player.BulletPref.name;
    }

    // Método para crear asteroides a intervalos de tiempo
    private void CreateEnemy()
    {
        time += Time.deltaTime;  // Aumenta el tiempo transcurrido
        if (time > spawnTime)    // Si ha pasado más tiempo que el tiempo de spawn de asteroides...
        {
            Instantiate(enemyPrefab,
                new Vector3(Random.Range(-8.0f, 8.0f), 7.0f, 0),
                Quaternion.identity);
            time = 0.0f;  // Reinicia el contador de tiempo de asteroides
        }
    }

    // Método para crear naves enemigas a intervalos de tiempo
    private void CreateNaveEnemiga()
    {
        naveTime += Time.deltaTime;  // Aumenta el tiempo transcurrido
        if (naveTime > naveSpawnTime)    // Si ha pasado más tiempo que el tiempo de spawn de naves...
        {
            Instantiate(naveEnemigaPrefab,
                new Vector3(Random.Range(-8.0f, 8.0f), 7.0f, 0),
                Quaternion.identity);
            naveTime = 0.0f;  // Reinicia el contador de tiempo de naves enemigas
        }
    }
}
