using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Cunanan, Joshua / McGee, Patrick]
 * Last Updated: [10/31/2023]
 * [This controls the Heavy Bullet functions]
 */

public class HeavyBullet : MonoBehaviour
{
    public float speed = 15f;
    public int damageDealt = 3;
    public bool goingLeft = false;


    // Update is called once per frame
    void Update()
    {
        Move();
    }

    /// <summary>
    /// Moves the bullet depending on the goingLeft bool.
    /// </summary>
    private void Move()
    {
        if (goingLeft)
        {
            transform.position += Vector3.left * Time.deltaTime * speed;
        }
        else
        {
            transform.position += Vector3.right * Time.deltaTime * speed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (other.gameObject.tag == "heavyWall")
        {
            other.gameObject.SetActive(false);
        }
    }
}
