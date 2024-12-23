using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Dropdown levelDropdown;
    public Image levelImage;
    public Sprite[] levelSprite;
    public Text percentageText;

    private float[] percentages = { 0.0f, 0.0f };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!PlayerPrefs.HasKey("percentages"))
        {
            SaveArrayToPlayerPrefs();
        }
        else
        {
            LoadArrayFromPlayerPrefs();
        }

        checkSelectedOption();
    }

    public void LoadLevel()
    {
        // Load the selected level
        //UnityEngine.SceneManagement.SceneManager.LoadScene("Level" + levelDropdown.value);
        if (levelDropdown.value == 0) UnityEngine.SceneManagement.SceneManager.LoadScene("LucaProves");
        else UnityEngine.SceneManagement.SceneManager.LoadScene("AlbertProves");
        PlayerPrefs.SetInt("level", levelDropdown.value);
    }

    public void ResetPercentages()
    {
        for (int i = 0; i < percentages.Length; i++)
        {
            percentages[i] = 0.0f;
        }
        SaveArrayToPlayerPrefs();
        checkSelectedOption();
    }


    public void checkSelectedOption()
    {
        int selectedOption = levelDropdown.value;
        levelImage.sprite = levelSprite[selectedOption];
        levelDropdown.value = selectedOption;

        percentageText.text = "Percentage: " + percentages[selectedOption] + "%";

    }

    private void SaveArrayToPlayerPrefs()
    {
        string arrayString = string.Join("/", System.Array.ConvertAll(percentages, x => x.ToString("F1", System.Globalization.CultureInfo.InvariantCulture)));
        PlayerPrefs.SetString("percentages", arrayString);
        PlayerPrefs.Save();
    }

    private void LoadArrayFromPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("percentages"))
        {
            string arrayString = PlayerPrefs.GetString("percentages");
            Debug.Log("Loaded percentages: " + arrayString);

            string[] stringArray = arrayString.Split('/');
            for (int i = 0; i < stringArray.Length; i++)
            {

                if (float.TryParse(stringArray[i], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float parsedValue))
                {
                    percentages[i] = parsedValue;
                }
                else
                {
                    Debug.LogWarning("Invalid percentage value found: " + stringArray[i]);
                    percentages[i] = 0.0f; 
                }
            }
        }
    }

}
