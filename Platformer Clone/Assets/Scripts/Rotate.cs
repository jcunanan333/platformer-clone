using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/*
 * Author: [Cunanan, Joshua]
 * Last Updated: [10/31/2023]
 * [This controls Samus's Rotation Movement]
 */

public class Rotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.Rotate(Vector3.up * 180);
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * Time.deltaTime * 5f;
        }
    }
}
