using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void InitLevelMenu()
    {
        // Load the level menu
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelMenu");
    }

    public void CloseGame()
    {
        // Close the game
        Application.Quit();
    }
}
