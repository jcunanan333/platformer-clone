using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * Author: [Cunanan, Joshua / McGee, Patrick]
 * Last Updated: [10/31/2023]
 * [This displays the health of the player on the screen during gameplay.]
 */

public class GameUIManager : MonoBehaviour
{

    public TMP_Text totalHealthText;
    public PlayerController PlayerController;

    // Update is called once per frame
    void Update()
    {
        totalHealthText.text = "Health: " + PlayerController.health;
    }
}
