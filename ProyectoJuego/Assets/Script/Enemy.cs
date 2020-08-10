using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

//Declaracion de variables

	//Variable que identifica la plataforma
	//Con la que se esta trabajando


	public GameObject platform;
	//variable para la velocidad
	public float speed;
	//Variable para guardar la transformacion del
	//Punto acrual donde estamos trabajando
	//que va a servir para el movimiento
	public Transform currentPoint;
	//Variable tipo vector que almacenara mas puntos
	public Transform[] points;
	//Variable que determinara la seleccion que se
	//esta utilizando
	public int pointSelection;

	// Use this for initialization
	void Start () {
		currentPoint = points[pointSelection];
	}
	
	// Update is called once per frame
	void Update () {
		//verificador de objeto existente
        		//que busca o permite tener movimiento
        		if (platform.gameObject ==null)
        		{
        			return;
        		}
        		//Se define el movimiento
        		platform.transform.position = Vector3.MoveTowards(
        		platform.transform.position,
        		currentPoint.position,
        		Time.deltaTime * speed);

        		//Condicion para saber la posicion actual de la plataforma
        		//Si son iguales pues al "pont Selection" se le suma 1. si el "pointSelection"
        		//Es igual a la cantidad de puntos de
        		//nuestro arreglo, "pointSelection" igual a cero.
        		if(platform.transform.position == currentPoint.position)
        		{
        			pointSelection += 1;
        			if(pointSelection == points.Length)
        			{
        				pointSelection = 0;
        				transform.localRotation = Quaternion.Euler(0,180,0);

        			}
        			else
        			{
        			pointSelection = 1;
        			transform.localRotation = Quaternion.Euler(0,0,0);
        			}
        			currentPoint = points[pointSelection];
        		}
	}
}
