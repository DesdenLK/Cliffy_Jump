using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void InitLevelMenu()
    {
        // Load the level menu
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelMenu");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
            UnityEngine.SceneManagement.SceneManager.LoadScene("LucaProves");
            PlayerPrefs.SetInt("level", 0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //UnityEngine.SceneManagement.SceneManager.LoadScene("Level2");
            UnityEngine.SceneManagement.SceneManager.LoadScene("AlbertProves");
            PlayerPrefs.SetInt("level", 1);
        }
    }

    public void CloseGame()
    {
        // Close the game
        Application.Quit();
    }
}
