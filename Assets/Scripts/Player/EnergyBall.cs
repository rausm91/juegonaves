using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnergyBall : Bullet
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
        public override void Movement() //trabaja con metodos del padre
        {
            transform.Translate(new Vector3(Mathf.Cos(Time.time * 1.5f), 1,0) * speed * Time.deltaTime);
        }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            Debug.Log("colisione con:" + collision.gameObject.name);

            if (collision.gameObject.CompareTag("Enemy"))
            {
                //GameManager.AddScore(10);
                Destroy(collision.gameObject);
                Destroy(this.gameObject);
            }

        }
    }

}
