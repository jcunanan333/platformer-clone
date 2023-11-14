using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Author: [McGee, Patrick / Cunanan, Joshua]
 * Last Updated: [11/02/2023]
 * [This file conatins the code that determines the behavior for RegularEnemy objects.]
 */


public class RegularEnemy : MonoBehaviour
{
    public float speed = 3f;
    public int hitPoints = 1;

    public GameObject leftPoint;
    public GameObject rightPoint;
    private Vector3 leftPos;
    private Vector3 rightPos;
    public bool goingLeft = true;

    // Start is called before the first frame update
    void Start()
    {
        leftPos = leftPoint.transform.position;
        rightPos = rightPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Die();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SmallBullet")
        {
            hitPoints -= other.gameObject.GetComponent<SmallBullet>().damageDealt;
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "HeavyBullet")
        {
            hitPoints -= other.gameObject.GetComponent<HeavyBullet>().damageDealt;
            other.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Moves the enemy between two points.
    /// </summary>
    private void Move()
    {
        if (goingLeft)
        {
            if (transform.position.x <= leftPos.x)
            {
                goingLeft = false;
            }
            else
            {
                transform.position += Vector3.left * Time.deltaTime * speed;
            }
        }
        else
        {
            if (transform.position.x >= rightPos.x)
            {
                goingLeft = true;
            }
            else
            {
                transform.position += Vector3.right * Time.deltaTime * speed;
            }
        }
    }

    /// <summary>
    /// Sets the enemy to inactive if hitpoints drop to 0 or below.
    /// </summary>
    private void Die()
    {
        if (hitPoints <= 0)
        {
            gameObject.SetActive(false);
            GameObject player = GameObject.Find("Samus");
            player.GetComponent<PlayerController>().IncreaseFinalEnemiesKilled();
        }
    }

}
