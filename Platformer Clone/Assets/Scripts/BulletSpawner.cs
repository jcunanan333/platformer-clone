using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Cunanan, Joshua / McGee, Patrick]
 * Last Updated: [10/31/2023]
 * [This controls the gun shooting]
 */

public class BulletSpawner : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;

 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.up * bulletSpeed;
        }
    }
}
