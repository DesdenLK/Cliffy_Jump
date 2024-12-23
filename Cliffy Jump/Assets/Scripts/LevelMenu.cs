using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Dropdown levelDropdown;
    public Image levelImage;
    public Sprite[] levelSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        checkSelectedOption();
    }

    public void LoadLevel()
    {
        // Load the selected level
        //UnityEngine.SceneManagement.SceneManager.LoadScene("Level" + levelDropdown.value);
        if (levelDropdown.value == 0) UnityEngine.SceneManagement.SceneManager.LoadScene("LucaProves");
        else UnityEngine.SceneManagement.SceneManager.LoadScene("AlbertProves");
    }


    public void checkSelectedOption()
    {
        int selectedOption = levelDropdown.value;
        levelImage.sprite = levelSprite[selectedOption];
        levelDropdown.value = selectedOption;
    }
}
