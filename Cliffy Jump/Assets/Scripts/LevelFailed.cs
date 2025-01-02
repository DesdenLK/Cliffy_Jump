using UnityEngine;
using UnityEngine.UI;

public class LevelFailed : MonoBehaviour
{
    public Text percentageText;

    public void Start()
    {
        percentageText.text = "You passed " + PlayerPrefs.GetFloat("percentage") + "% of the level";
    }
    public void Retry()
    {
        if (PlayerPrefs.GetInt("level") == 0) UnityEngine.SceneManagement.SceneManager.LoadScene("Nivell1");
        else UnityEngine.SceneManagement.SceneManager.LoadScene("Nivell2");
    }
    public void BackToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelMenu");
    }
}
