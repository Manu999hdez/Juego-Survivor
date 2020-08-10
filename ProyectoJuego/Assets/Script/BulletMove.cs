using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour {

	//Variable de control de giro del player

	public GameObject player;

	private Transform playerTrans;

	private Rigidbody2D bulletRB;

	// private Rigidbody2D rb;

	//variable control de velocidad
	public float bulletSpeed;

	//Variable de tiempo de vida de la bala
	public float bulletLife;

	//--
	public static int damage;
	public int damageRef;

	public GameObject healthBar;
	private float curHealth;

	//Antes de que empieze la funcion los valores esten configurados

	void Awake()
	{
		bulletRB = GetComponent<Rigidbody2D>();
		player = GameObject.FindGameObjectWithTag("Player");
		playerTrans = player.transform;
	}

	// Use this for initialization
	void Start () {
		if(playerTrans.rotation.y == 0)
		{
			bulletRB.velocity = new Vector2(bulletSpeed, bulletRB.velocity.y);
		}else
		{
			bulletRB.velocity = new Vector2(-bulletSpeed, bulletRB.velocity.y);
		}

	}

	// Update is called once per frame
	void Update () {
		Destroy(gameObject, bulletLife);


	}

	void OnCollisionEnter2D(Collision2D obj)
		{
			//Condicion que determinara que objeto se va a eliminar
			//Tomando como referencia el nombre del objeto con el
			//Cual vamos a colisionar

			if(obj.transform.name == "top" || obj.transform.name == "body")
			{
				Destroy(obj.transform.parent.gameObject);
				Destroy(this.gameObject);
				//PracticleSystem.DestroyObject(this.gameObject);
				return;
			}
		}

		public void OnTriggerEnter2D(Collider2D col)
        		{
        		     if(col.tag == "Platform" || col.tag == "Enemy")
        		     {
        		        GetComponent<SpriteRenderer>().enabled = false;
        		        GetComponent<CircleCollider2D>().enabled = false;
        		     }
        		}
		}
