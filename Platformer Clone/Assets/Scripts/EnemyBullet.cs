using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Cunanan, Joshua / McGee, Patrick]
 * Last Updated: [10/31/2023]
 * [This controls the enemy bullets]
 */
public class EnemyBullet : MonoBehaviour
{
    public float speed = 3f;
    public Vector3 playerPosition;

    private void Start()
    {
        //transform.rotation = Quaternion.LookRotation(playerPosition);
        transform.LookAt(playerPosition);
        transform.Rotate(new Vector3(90f,0,0));
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    /// <summary>
    /// Controls how the bullet moves.
    /// </summary>
    private void Move()
    {

        transform.position += transform.up * Time.deltaTime * speed;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ShootingEnemy")
        {
            
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
