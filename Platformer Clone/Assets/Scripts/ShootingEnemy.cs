using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/*
 * Author: [Cunanan, Joshua / McGee, Patrick]
 * Last Updated: [10/31/2023]
 * [This controls the shooting enemy.]
 */
public class ShootingEnemy : MonoBehaviour
{

    public int hitPoints = 1;

    public float radius = 10f;

    public GameObject bullet;

    public Vector3 playerPosition;

    private bool playerIsNear = false;

    private bool canShoot = true;

    // Update is called once per frame
    void Update()
    {
        SearchForPlayer();
        Die();
    }

    /// <summary>
    /// Checks an OverlapSphere for collided objects. If the player is one of them, starts the Shooting coroutine towards their position.
    /// </summary>
    private void SearchForPlayer()
    {

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        
        foreach (Collider col in colliders) 
        {
            if (col.transform.gameObject.tag == "Player")
            {
                
                if (playerIsNear == false)
                {
                    playerIsNear = true;
                }

                playerPosition = col.transform.position;
                StartCoroutine(Shooting());
            }
        }
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
    /// Handles instantiating EnemyBullets.
    /// </summary>
    private void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);
        newBullet.GetComponent<EnemyBullet>().playerPosition = playerPosition;

    }

    /// <summary>
    /// Sets the gameobject inactive is hitpoints is 0 or below.
    /// </summary>
    private void Die()
    {
        if (hitPoints <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// A coroutine that puts a cooldown on the enemy's shooting.
    /// </summary>
    /// <returns></returns>
    IEnumerator Shooting()
    {
        if (playerIsNear && canShoot)
        {
            canShoot = false;
            Shoot();
            yield return new WaitForSeconds(3f);
            canShoot = true;
        }
    }

}
