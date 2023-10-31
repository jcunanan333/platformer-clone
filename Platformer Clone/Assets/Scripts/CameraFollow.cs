using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Cunanan, Joshua]
 * Last Updated: [10/31/2023]
 * [This controls the camera being locked onto the player]
 */


public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    private Vector3 offset;
    // Start is called before the first frame update
    private void Start()
    {
        //transform position of the camera - transform position of the target
        offset = transform.position - target.transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        //as the target/player moves we add offset to this object
        transform.position = target.transform.position + offset;
    }
}
