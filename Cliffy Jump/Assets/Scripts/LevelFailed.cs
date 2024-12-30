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
        if (PlayerPrefs.GetInt("level") == 0) UnityEngine.SceneManagement.SceneManager.LoadScene("LucaProves");
        else UnityEngine.SceneManagement.SceneManager.LoadScene("AlbertProves");
    }
    public void BackToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelMenu");
    }
}
