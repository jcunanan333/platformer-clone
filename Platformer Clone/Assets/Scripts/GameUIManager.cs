using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * Author: [Cunanan, Joshua]
 * Last Updated: [11/03/2023]
 * [This controls the game ui]
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
