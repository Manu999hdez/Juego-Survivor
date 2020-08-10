using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Libreria para los textos
using UnityEngine.UI;
//Manejador de escenas
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
	//Variables para almacenar los componentes
	private Rigidbody2D rb;
	private Animator animator;
	//Variables de control
	
	//variable de la mascara con la que se esta trabajando 
	public LayerMask maskFloor;
	//Variable de testeo del piso
	public Transform testFloor;
	//Variable de la fuerza del salto
	public float forceJump = 7f;
	//Variables que ayuda a saber cuando esta caminando
	private bool isWalking;
	//Variable para saber si esta en el suelo
	private bool isFloor = true;
	//Variable del radio que se va a trabajar 
	private float radio = 0.07f;
	//variable para controlar el salto del personaje
	private bool jump2 = false;
	//Variable que controla la velocidad
	private float speed = 3f;
	//Variable factor que permite dentro del juego acelerar
	public float factor = 1f;
	//Variable que controla la ubicacion inicial 
	private Vector2 posINI;
	//Variables de la tabla
	//Variable para el control de texto
    	public Text txtScore;
    	private int score;
	public Transform bulletSpawner;
	public GameObject bulletPrefab;
	//Variable
	private Transform rbrazo;
	//Variable oara avanzar a la izquierda
	private bool avanzarIzq = false;
	private bool avanzarDer = false;
	private bool saltarr = false;
	private bool  correrDisp = false;

	// Use this for initialization
	void Start () {
		rb= GetComponent<Rigidbody2D>();
        		animator = GetComponent<Animator>();
        		posINI = transform.position;

        		//inicializando el Texto
        		txtScore.text = "SCORE: " + score.ToString();
	}
	
	//Metodo que vincula las fisicas en Unity
	
	void FixedUpdate()
	{
		//Detectar cuando el personaje se encuentra en el piso
		//La idea es que cuando colisione el "GameObject"
		//"testFloor" con cualquier elemento, sepamos que estamos en el piso 
		isFloor = Physics2D.OverlapCircle(testFloor.position, radio, maskFloor);
		//Colocar uno de los valores con los que se esta trabajando
		animator.SetBool("isJump", !isFloor);
		
		//La idea es que se  pueda controlar el doble salto, no de forma constante
		//Sino que cada vez que termine el salto, o sea, colisione con el piso,
		//Recien pueda volver a saltar
		if (isFloor)
		{
			//jump2 = false;
		}
		
		//Condicion para cuando se cae el personaje 
		//Que retorne a la posicion inicial
		if(transform.position.y < -15)
		{
			transform.position = posINI;
		}

		if(avanzarIzq == true)
		{
		            animator.SetBool("isWalk", true);
        			isWalking = true;
        			//Rotar en grados al personaje en los ejes X,Y,Z
        			transform.localRotation = Quaternion.Euler(0,180,0);
        			//Velocidad al caminar a la izquierda
        			speed = -7;
        			factor = 1;
		}

		else
		{
		//
		NoAvanzar();
		}
		if(avanzarDer == true)
                		{
                		     animator.SetBool("isWalk", true);
                             isWalking = true;
                             //Rotar en grados al personaje en los ejes X,Y,Z
                             transform.localRotation = Quaternion.Euler(0,0,0);
                             //Velocidad al caminar a la derecha
                             speed = 7;
                             factor = 1;
                        }
        if(saltarr == true)
        {
         if(isFloor || !jump2)
         			{

         			    //Cambiando la velocidad e nuestro "RigidBody" para que no se

         				//Aumente o se incremente consecutivamente la velocidad
         				rb.velocity = new Vector2(rb.velocity.x, forceJump);
         			    rb.AddForce(new Vector2(0, forceJump));

         				//Condiciones del salto
         				if(!jump2 && !isFloor)
         				{
         					jump2 = true;
         				}
         			}
        }

        if(correrDisp == true && isWalking)
        {
        factor = 3;
        }

        if(correrDisp == false && isWalking)
        {
        factor = 1;
        }
	}
	
	// Update is called once per frame
	void Update () {
		PlayerShooting();
		//-----
        		if(Input.GetKeyDown(KeyCode.X))
        		{
        		animator.SetBool("isShoot", true);
        		}
        		if(Input.GetKeyUp(KeyCode.X))
        		{
        		animator.SetBool("isShoot",false);
        		}

		//Vamos a hacer nosotros es aplicar una fuerza
		//Para que se mueva el  personaje
		if(Input.GetKeyDown(KeyCode.W))
		{
			/*if(isFloor || !jump2)
			{
				
			    //Cambiando la velocidad e nuestro "RigidBody" para que no se 
			
				//Aumente o se incremente consecutivamente la velocidad
				rb.velocity = new Vector2(rb.velocity.x, forceJump);
			    rb.AddForce(new Vector2(0, forceJump));
				
				//Condiciones del salto
				if(!jump2 && !isFloor)
				{
					jump2 = true;
				}
			}*/
			Saltarr();
		}
		
		//Condion para qye el personaje camine a la izquierda
		if(Input.GetKeyDown(KeyCode.A))
		{
		/*	animator.SetBool("isWalk", true);
			isWalking = true;
			//Rotar en grados al personaje en los ejes X,Y,Z
			transform.localRotation = Quaternion.Euler(0,180,0);
			//Velocidad al caminar a la izquierda
			speed = -7;
			factor = 1;*/
			AvanzarIzq();
		}
		if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
		{
			/*animator.SetBool("isWalk", false);
			isWalking = false;*/
			DejarDeAvanzaIzq();
			DejarDeAvanzaDer();
		}
		
		//Condicion de la tecla shift presionada
        		if(Input.GetKeyDown(KeyCode.LeftShift) && isWalking)
        		{
        			factor = 3;
        		}

        		//Condicion de la tecla shift sin presionar
        		if(Input.GetKeyUp(KeyCode.LeftShift) && isWalking)
        		{
        			factor = 1;
        		}

        		//Condicion para saber si esta caminando
        		//para caminar la velocidad al RigidBody2D
        		if (isWalking)
        		{
        			rb.velocity = new Vector2(speed * factor, rb.velocity.y);
        		}
		
		//Condicion para caminar 
		if(Input.GetKeyDown(KeyCode.D))
		{
			/*animator.SetBool("isWalk", true);
			isWalking = true;
			//Rotar en grados al personaje en los ejes X,Y,Z
			transform.localRotation = Quaternion.Euler(0,0,0);
			//Velocidad al caminar a la derecha
			speed = 7;
			factor = 1;*/
			AvanzarDer();
		}
		//if(Input.GetKeyUp(KeyCode.D))
		//{
		//	animator.SetBool("isWalk", false);
		//	isWalking = false;
		//}
		
	}
	//Trabajando con las collisiones
	//Creando el metodo que se hara cargo de las colisiones
	//Cuando entre el objeto
	
	void OnCollisionEnter2D(Collision2D obj)
	{
		//Condicion si esta en la plataforma
		if(obj.transform.tag == "move")
		{
			//metemos al personaje dentro de la plataforma
			//para que se mueva a su misma velocidad
			transform.parent = obj.transform;
		}
		
		//condicion que detemrinara que objeto se va a eliminar 
		//tomando como referencia el nombre del objeto con el 
		//cual vamos a colisionar
		if(obj.transform.name == "top")
		{
			Destroy(obj.transform.parent.gameObject);
			return;
		}

		if(obj.transform.name == "body")
        		{
        			//destruyendo el player
        			Destroy(this.gameObject);
        			//Cargar escena
        			SceneManager.LoadScene("Perdiste");
        		}

        		//Condicion para destruir objeto "Gema"
                		//Si ocurre una colision
                		if (obj.transform.tag == "Gema")
                		{
                			//destruir el objeto ----> gema
                			Destroy(obj.transform.gameObject);

                			//Contador de colisiones
                			score += 1;
                			//Actualizacion del score
                			txtScore.text = "SCORE: " + score.ToString();
                		}

                		//Condicion para comprobar colisiones con la puerta
                		if(obj.transform.tag == "door")
                		{
                			//Muestre mensaje "Ganaste"
                			print("GANASTE!!!");
                			SceneManager.LoadScene("Ganaste");
                		}
                		//Condicion para destruir objeto "caja esconde gema"
                		//Si ocurre una coalision
                		if(obj.transform.tag == "BoxSecret")
                		{
                			//Destruir el objeto ------> Box
                			Destroy(obj.transform.gameObject);
                		}

	}
	
	//Creando el metodo que se hara cargo de las colisiones
	//cuando salga el objeto
	
	void OnCollisionExit2D(Collision2D obj)
	{
		//Cuando sale de la plataforma el personaje
		transform.parent = null;
	}

	public void PlayerShooting()
	{
	       if(Input.GetButtonDown("Fire1"))
	       {
	          //Copia del objeto
	          Instantiate(bulletPrefab, bulletSpawner.position, bulletSpawner.rotation);
	       }
	}
	public void CorrerDisp()
	{
	correrDisp = true;
	}
	public void NoCorrerDisp()
	{
	correrDisp = false;
	}
    public void Saltarr()
    {
    saltarr = true;
    }
    public void NoSaltarr()
    {
    saltarr = false;
    }
	public void AvanzarDer()
	{
	avanzarDer = true;
	}

    public void DejarDeAvanzaDer()
    {
      avanzarDer = false;
    }
	public void AvanzarIzq()
	{
	avanzarIzq = true;
	}

	public void DejarDeAvanzaIzq()
	{
	avanzarIzq = false;

	}

	private void NoAvanzar()
	{
	    if(avanzarIzq == false || avanzarDer == false)
	    {
	      animator.SetBool("isWalk", false);
          isWalking = false;
	    }
	}
}
