using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Cunanan, Joshua / McGee, Patrick]
 * Last Updated: [10/31/2023]
 * [This controls the small bullet functions]
 */

public class SmallBullet : MonoBehaviour
{
    public float speed = 15f;
    public int damageDealt = 1;
    public bool goingLeft = false;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    /// <summary>
    /// Controls movement depending on the way the bullet is facing.
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
    }

}
