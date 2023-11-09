using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [McGee, Patrick]
 * Last Updated: [11/02/2023]
 * [This file contains the code that determines the behavior for HardEnemy objects.]
 */

public class HardEnemy : MonoBehaviour
{
    public int damageDealt = 7;
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

    private void Die()
    {
        if (hitPoints <= 0)
        {
            gameObject.SetActive(false);
        }
    }

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
