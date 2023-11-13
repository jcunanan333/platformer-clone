using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Author: [Cunanan, Joshua / McGee, Patrick]
 * Last Updated: [10/31/2023]
 * [This controls the menu ui:quit game/change scene]
 */

public class MenuUIManager : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
