using UnityEngine;
using UnityEngine.UI;

public class LevelFailed : MonoBehaviour
{
    public Text percentageText;
    private AudioSource levelFailedSound;

    public void Start()
    {
        percentageText.text = "You passed " + PlayerPrefs.GetFloat("percentage") + "% of the level";
        levelFailedSound = gameObject.AddComponent<AudioSource>();
        levelFailedSound.clip = Resources.Load<AudioClip>("Sounds/levelFailed");
        levelFailedSound.playOnAwake = false;
        levelFailedSound.loop = false;
        levelFailedSound.Play();
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
