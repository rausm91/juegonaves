using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // Variables
    public float speed = 10.0f;
    public float fireRate = 0.25f;
    public int lives = 3;
    public int shieldsAmount = 3;
    public float canFire = 0.0f;
    public float shieldDuration = 50f;
    public GameObject BulletPref;
    public GameObject shield;
    public int actualWeapon = 0;

    public AudioManager audioManager;
    public AudioSource actualAudio;


    public enum ShipState
    {
        FullHealth,
        SlightlyDamaged,
        Damaged,
        HeavilyDamaged,
        Destroyed
    }

    public ShipState shipState;
    public List<Sprite> shipSprites = new List<Sprite>();

    void ChangeShipState()
    {
        var currentState = shipState;
        Debug.Log(currentState);

        var newSprite = shipSprites.Find(x => x.name == currentState.ToString());

        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = newSprite;

        switch (currentState)
        {
            case ShipState.FullHealth:
                shipState = ShipState.SlightlyDamaged;  // Cambiado a SlightlyDamaged
                break;
            case ShipState.SlightlyDamaged:
                shipState = ShipState.Damaged;
                break;
            case ShipState.Damaged:
                shipState = ShipState.HeavilyDamaged;
                break;
            case ShipState.HeavilyDamaged:
               
                break;
            
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        shield.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CheckBoundaries();
        ChangeWeapon();
        UseShields();
        Fire();
        

    }


    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
        transform.Translate(Vector3.up * speed * verticalInput * Time.deltaTime);
    }

    void CheckBoundaries()
    {
        var cam = Camera.main;
        float xMax = cam.orthographicSize + cam.aspect;
        float yMax = cam.orthographicSize;
        if (transform.position.x > xMax)
        {
            transform.position = new Vector3(-xMax, transform.position.y, 0);
        }
        else if (transform.position.x < -xMax)
        {
            transform.position = new Vector3(xMax, transform.position.y, 0);
        }

        if (transform.position.y > yMax)
        {
            transform.position = new Vector3(transform.position.x, -yMax, 0);
        }
        else if (transform.position.y < -yMax)
        {
            transform.position = new Vector3(transform.position.x, yMax, 0);
        }

    }
    void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > canFire)
        {

            //Instantiate(BulletPref, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            //canFire = Time.time + fireRate;
            //actualAudio = audioManager.GetAudio("Bullet")
            //actualAudio.Play();
            //actualAudio.pitch = Random.Range(0.8f, 1.2f);
            
            switch(BulletPref.name) //buscar por prefab 
            {
                case "Bullet":
                    Instantiate(BulletPref, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
                    canFire = Time.time + fireRate;
                    actualAudio.pitch = 1;
                    actualAudio.Play();
                    break;

                case "Missile":  //nombre del prefab

                    Debug.Log("Firing triple missiles");

                    var bullet1 = Instantiate(BulletPref, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
                    bullet1.GetComponent<Missile>().direction = Vector2.up;

                    var bullet2 = Instantiate(BulletPref, transform.position + new Vector3(0.5f, 0.8f, 0), Quaternion.identity);
                    bullet2.GetComponent<Missile>().direction = new Vector2(0.5f,1);

                    var bullet3 = Instantiate(BulletPref, transform.position + new Vector3(-0.5f, 0.8f, 0), Quaternion.identity);
                    bullet3.GetComponent<Missile>().direction = new Vector2(-0.5f,1);

                    canFire = Time.time + fireRate;
                    actualAudio.pitch = 0.5f;
                    actualAudio.Play();
                    break;

                case "Magic":
                    Debug.Log("Firing Magic Bullet");
                    // Instanciar el MagicBullet
                    Instantiate(BulletPref, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
                    canFire = Time.time + fireRate;
                    actualAudio.pitch = 0.25f;
                    actualAudio.Play();
                    break;


            }

        }

    }
    public List<Bullet> bullets;

    public void ChangeWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            BulletPref = bullets[0].gameObject;
            actualWeapon = 0;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            BulletPref = bullets[1].gameObject;
            actualWeapon = 1;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            BulletPref = bullets[2].gameObject;
            actualWeapon = 2;
        }
        
    }

    void UseShields()
    {
        if(Input.GetKeyDown(KeyCode.Z)&& shieldsAmount > 0)
        {
            shieldsAmount--;
            shield.SetActive(true);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        if (shield.activeSelf)
        {
            shieldDuration -= Time.deltaTime;
            if (shieldDuration < 0)
            {
                shield.SetActive(false);
                shieldDuration = 5.0f;
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
        }

    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision !=null)
        {
            if(collision.gameObject.CompareTag("Enemy"))
            {
                // destruiye el objeto
                //Destroy(collision.gameObject);
                //destruye el personaje
                //Destroy(this.gameObject);
                ChangeShipState();

                if (lives > 1) 
                {
                    lives--;
                    Debug.Log("lives: " + lives);
                }
                else
                {
                    lives--;
                    Destroy(this.gameObject);
                    ChangeShipState();

                }


                Debug.Log("player colision con el enemigo");
            }
        }
    }

        

}
