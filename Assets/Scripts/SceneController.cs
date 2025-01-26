using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void StartGame()
    {
       SceneManager.LoadScene("Game");      
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}