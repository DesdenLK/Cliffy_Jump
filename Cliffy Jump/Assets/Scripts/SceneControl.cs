using System;
using System.Collections.Generic;
using UnityEngine;

public class SceneControl : MonoBehaviour
{
   
    public GameObject player;


    public GameObject[] miniLevels; 

    private PathRise PathRise;
    private PathFalling PathFalling;
    private GameObject Ground;

    private AutoJump AutoJump;

    private int indexLevel = 0;

    bool playerInit = false;
    void Start()
    {
        player.GetComponent<PathFollower>().enabled = false;
        AutoJump = player.GetComponent<AutoJump>();
        initGround();

        miniLevels[indexLevel].SetActive(true);
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
                        Debug.Log("Game Over");
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
}
