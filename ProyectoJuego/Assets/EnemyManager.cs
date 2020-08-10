using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

public int enemyHealth;
public int enemyValue;

public GameObject healthBar;
private float curHealth;


public void OnTriggerEnter2D(Collider2D col)
{
    if(col.tag == "Bullet")
    {

    curHealth -= BulletMove.damage;
    float barLength = curHealth / enemyHealth;
    SetHealthBar(barLength);

    if(enemyHealth <= 0)
    {
    Destroy(gameObject);
    return;
    }
    }
}
	// Use this for initialization
	void Start () {
		curHealth = enemyHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
public void SetHealthBar(float eHealth)
{
     healthBar.transform.localScale = new Vector3(eHealth, healthBar.transform.localScale.y,
     healthBar.transform.localScale.z);
}

}
