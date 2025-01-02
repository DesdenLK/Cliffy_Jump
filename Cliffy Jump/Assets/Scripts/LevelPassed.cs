using UnityEngine;

public class LevelPassed : MonoBehaviour
{
    private AudioSource levelPassedSound;

    private void Start()
    {
        levelPassedSound = gameObject.AddComponent<AudioSource>();
        levelPassedSound.clip = Resources.Load<AudioClip>("Sounds/levelPassed");
        levelPassedSound.playOnAwake = false;
        levelPassedSound.loop = false;
        levelPassedSound.Play();
    }
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
