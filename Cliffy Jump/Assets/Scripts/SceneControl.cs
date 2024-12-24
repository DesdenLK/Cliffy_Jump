using System;
using System.Collections.Generic;
using UnityEngine;

public class SceneControl : MonoBehaviour
{
   
    public GameObject[] players;
    private GameObject player;


    public GameObject[] miniLevels; 

    private PathRise PathRise;
    private PathFalling PathFalling;
    private GameObject Ground;

    private AutoJump AutoJump;

    private int indexLevel = 0;
    private float[] percentages = new float[2];

    bool playerInit = false;
    void Start()
    {
        player = players[PlayerPrefs.GetInt("player")];
        player.SetActive(true);
        player.GetComponent<PathFollower>().enabled = false;
        AutoJump = player.GetComponent<AutoJump>();
        initGround();

        miniLevels[indexLevel].SetActive(true);
        LoadArrayFromPlayerPrefs();
    }

    public void Reset()
    {
        player.GetComponent<PathFollower>().enabled = false;
        
        playerInit = false;
        indexLevel = 0;
        miniLevels[indexLevel].SetActive(true);
        initGround();
        GetComponent<Distance_Percentage>().resetDistance();
    }

    void initPlayer()
    {
        Transform[] path = getChildrenTransform(miniLevels[indexLevel].transform.Find("PointsPath").gameObject);
        player.GetComponent<PathFollower>().Points = path;

        Transform[] autoJump = getChildrenTransform(miniLevels[indexLevel].transform.Find("AutoJump").gameObject);
        player.GetComponent<AutoJump>().Points = autoJump;

        player.GetComponent<PathFollower>().enabled = false;

        player.GetComponent<PathFollower>().initPathFollower();
        GetComponent<Distance_Percentage>().setPlayerPosition(player.transform);
        player.GetComponent<AutoJump>().initAutoJump();

        if (PathRise != null)
        {
            if (PathRise.isItFinished())
            {
                player.GetComponent<PathFollower>().enabled = true;
                playerInit = true;
                PathRise.enabled = false;
                Debug.Log("Player initialized");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerInit) initPlayer();
        else
        {
            if (player.GetComponent<PathFollower>().isPlayerFinished())
            {   
                PathFalling.enabled = true;
                if (PathFalling != null && PathFalling.isItFinished())
                {
                    miniLevels[indexLevel].SetActive(false);
                    indexLevel++;
                    if (indexLevel < miniLevels.Length)
                    {
                        miniLevels[indexLevel].SetActive(true);
                        initGround();
                        playerInit = false;
                    }
                    else
                    {
                        setPercentage(100f);
                        Debug.Log("Game Over");
                        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelPassed");
                    }
                }
            }
        }




        if (Input.GetKeyDown(KeyCode.G))
        {
            AutoJump.setAutoJump(!AutoJump.getAutoJump());
            Debug.Log("AutoJump " + AutoJump.getAutoJump());
        }
    }

    void initGround()
    {
        Ground = miniLevels[indexLevel].transform.Find("Ground").gameObject;
        Debug.Log(Ground);
        PathRise = Ground.GetComponent<PathRise>();
        PathFalling = Ground.GetComponent<PathFalling>();
        PathFalling.enabled = false;

    }

    Transform[] getChildrenTransform(GameObject parent)
    {
        List<Transform> childTransforms = new List<Transform>();
        foreach (Transform child in parent.transform)
        {
            childTransforms.Add(child);
        }
        return childTransforms.ToArray();
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

    public void setPercentage(float percentage)
    {
        percentages[PlayerPrefs.GetInt("level")] = percentage;
        SaveArrayToPlayerPrefs();
    }

    public float getPercentage()
    {
        return percentages[PlayerPrefs.GetInt("level")];
    }

    public GameObject getPlayer()
    {
        return player;
    }
}
