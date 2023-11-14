using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [McGee, Patrick / Cunanan, Joshua]
 * Last Updated: [11/02/2023]
 * [This file contains the code that determines the behavior for HardEnemy objects.]
 */

public class HardEnemy : MonoBehaviour
{
    public float speed = 5f;
    public int hitPoints = 10;

    public GameObject followTarget;

    // Start is called before the first frame update
    void Start()
    {
        
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
    /// Causes the enemy to die at 0 hitPoints and increases the variable on samus for the number of dead enemies on the final level.
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

    /// <summary>
    /// Controls enemy movement relative to the player.
    /// </summary>
    private void Move()
    {
        if (followTarget.transform.position.x <= transform.position.x)
        {
            transform.position += Vector3.left * Time.deltaTime * speed;
        }
        if (followTarget.transform.position.x >= transform.position.x)
        {
            transform.position += Vector3.right * Time.deltaTime * speed;
        }
    }
}
