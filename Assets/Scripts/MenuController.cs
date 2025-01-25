using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public void StartGame()
    { //Not sure why it doesn't allow my to just do SceneManager.LoadScene here
       UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public void GoToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}