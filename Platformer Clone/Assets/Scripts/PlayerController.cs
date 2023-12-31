using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Author: [Cunanan, Joshua / McGee, Patrick]
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
    private GameObject playerBody;

    public bool goingLeft = false;
    public bool rotateLeft = false;

    //location of where the player respawns to
    private Vector3 startPos;

    //Variables for taking damage
    public bool isInvuln = false;


    //Variables for shooting
    public bool gunOnCooldown = false;
    public bool isHeavyWeapon = false;
    public GameObject smallBullet;
    public GameObject heavyBullet;
    public GameObject bulletSource;

    public int stage = 0;
    public int finalEnemiesKilled = 0;

    // Start is called before the first frame update
    void Start()
    {
        //set the startPos
        startPos = transform.position;

        //set the reference to the player's attached rigidbody
        playerRigidbody = GetComponent<Rigidbody>();
        playerBody = transform.GetChild(0).transform.gameObject;
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
        HandleShooting();
        CheckVictory();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Portal")
        {
            transform.position = other.GetComponent<Portal>().spawnPoint.transform.position;
            startPos = other.GetComponent<Portal>().spawnPoint.transform.position;
            stage++;
        }
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
            jumpForce *= 2f;
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "RegularEnemy")
        {
            HandleDamage(15);
        }
        if (other.gameObject.tag == "HardEnemy")
        {
            HandleDamage(35);
        }
        if (other.gameObject.tag == "HeavyPickup")
        {
            isHeavyWeapon = true;
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "EnemyBullet")
        {
            HandleDamage(25);
        }
    }

    /// <summary>
    /// Increases the number of enemies killed if on the final level.
    /// </summary>
    public void IncreaseFinalEnemiesKilled()
    {
        if (stage == 3)
        {
            finalEnemiesKilled++;
        }
        
    }

    /// <summary>
    /// Checks to see if 8 enemies have been killed on the final level. This value is hard-coded to the number of enemies placed on the final level.
    /// </summary>
    private void CheckVictory()
    {
        if (finalEnemiesKilled >= 8)
        {
            SceneManager.LoadScene(3);
        }
    }

    /// <summary>
    /// Handles taking damage.
    /// </summary>
    /// <param name="damage">How much damage Samus should take.</param>
    private void HandleDamage(int damage)
    {
        if (!isInvuln)
        {
            health -= damage;

            if (health <= 0)
            {
                Die();
            }

            isInvuln = true;
            StartCoroutine(Blink());
        }

        
    }

    /// <summary>
    /// A coroutine that makes samus invulnerable and blink for 5 seconds.
    /// </summary>
    /// <returns></returns>
    public IEnumerator Blink()
    {
        for (int index = 0; index < 10; index++)
        {
            if (index % 2 == 0)
            {
                playerBody.SetActive(false);
            }
            else
            {
                playerBody.SetActive(true);
            }
            yield return new WaitForSeconds(.5f);
        }
        isInvuln = false;
        playerBody.SetActive(true);
    }

    /// <summary>
    /// The code necessary to spawn bullets and point them in the right direction.
    /// </summary>
    private void HandleShooting()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            if (!gunOnCooldown)
            {
                if (!isHeavyWeapon)
                {
                    GameObject newBullet = Instantiate(smallBullet, bulletSource.transform.position, heavyBullet.transform.rotation);
                    newBullet.GetComponent<SmallBullet>().goingLeft = goingLeft;
                    StartCoroutine(ShootingCooldown());
                }
                else
                {
                    GameObject newBullet = Instantiate(heavyBullet, bulletSource.transform.position, heavyBullet.transform.rotation);
                    newBullet.GetComponent<HeavyBullet>().goingLeft = goingLeft;
                    StartCoroutine(ShootingCooldown());
                }
            }
        }
    }

    /// <summary>
    /// A coroutine that puts the gun on cooldown after firing, preventing continuous firing.
    /// </summary>
    /// <returns></returns>
    public IEnumerator ShootingCooldown()
    {
        gunOnCooldown = true;
        yield return new WaitForSeconds(.5f);
        gunOnCooldown = false;
    }

    /// <summary>
    /// Sends player to the GameOver scene when health is 0 or lower.
    /// </summary>
    private void Die()
    {
        //check if player has 0 health
        if (health <= 0)
        {
            SceneManager.LoadScene(2);
        }
    }

    /// <summary>
    /// Handles input for jumping by checking a raycast hit.
    /// </summary>
    private void HandleJumping()
    {
        //handles jumping
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            RaycastHit hit;



            //if the raycast returns true then an object has been hit and the player is touching the floor
            //for raycast (startPosition, RayDirection, output the object hit, maximumDistanceForTheRaycastToFire
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.1f))
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

    /// <summary>
    /// Rotates Samus depending on how she's facing.
    /// </summary>
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
