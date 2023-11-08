using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Author: [Cunanan, Joshua]
 * Last Updated: [10/31/2023]
 * [This controls all the players movement and functions]
 */


public class PlayerController : MonoBehaviour
{
    //this will determine how much health the player has
    public int health = 99;
    //side to side movement speed
    public float speed = 10f;
    //jump force added when the player presses space
    public float jumpForce = 2f;

    private Rigidbody playerRigidbody;

    public bool goingLeft = true;
    public bool rotateLeft = false;

    //location of where the player respawns to
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        //set the startPos
        startPos = transform.position;

        //set the reference to the player's attached rigidbody
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //left and right player movement
        if (Input.GetKey(KeyCode.A))
        {
            goingLeft = true;
            Turning();
        }
        if (Input.GetKey(KeyCode.D))
        {
            goingLeft = false;
            Turning();

        }

        HandleJumping();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ExtraHealthPickup")
        {
            health = 199;
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "HealthPickup")
        {
            health += other.gameObject.GetComponent<HealthPickup>().healthRegained;
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "JumpPickup")
        {
            jumpForce *= 2;
            other.gameObject.SetActive(false);
        }
    }



    private void Die()
    {
        //check if player has 0 health
        if (health == 0)
        {
            SceneManager.LoadScene(2);
        }
    }
    private void HandleJumping()
    {
        //handles jumping
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            RaycastHit hit;



            //if the raycast returns true then an object has been hit and the player is touching the floor
            //for raycast (startPosition, RayDirection, output the object hit, maximumDistanceForTheRaycastToFire
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f))
            {
                Debug.Log("Touching the ground");
                //adds an upwards velocity to the player object causing the player to jump
                playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            else
            {
                Debug.Log("Can't jump, not touching the ground");
            }
        }

    }
    private void Turning()
    {
        if (goingLeft == false)
        {
            if (rotateLeft == true)
            {
                transform.Rotate(Vector2.up * 180);
                rotateLeft = false;
            }
            transform.position += Vector3.right * speed * Time.deltaTime;

        }
        else
        {
            if (rotateLeft == false)
            {
                transform.Rotate(Vector2.up * 180);
                rotateLeft = true;
            }
            transform.position += -Vector3.right * speed * Time.deltaTime;


        }
    }
}
