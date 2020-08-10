using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	//variable publica para los datos de transformacion de nuestro ?player?
	public Transform player;
	//variable para modificar desde el editor de unity
	public float offset = 6f;

	// Use this for initialization
//	void Start () {
		
//	}
	
	// Update is called once per frame
	void Update () {
		//Evitando que fuerce el resto del codigo
		//Debido a colisiones con el personaje
		if (player == null)
		{
			return;
		}
		//Cambiando la posicion de la camara en X;Y;Z
		//de forma horizontal en el eje Y
		transform.position = new Vector3(player.position.x + offset, transform.position.y, transform.position.z);
	}
	
	
}
