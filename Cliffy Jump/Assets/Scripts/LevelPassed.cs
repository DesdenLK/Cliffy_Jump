using UnityEngine;

public class LevelPassed : MonoBehaviour
{
    public void loadMenu()
    {
        if (PlayerPrefs.GetInt("level") < 1)
        {
            // Load the main menu
            UnityEngine.SceneManagement.SceneManager.LoadScene("LevelMenu");
        }
        else
        {
            // Load credits
            UnityEngine.SceneManagement.SceneManager.LoadScene("Credits");
        }
    }
}
