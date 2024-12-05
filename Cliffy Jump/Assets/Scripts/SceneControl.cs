using System;
using UnityEngine;

public class SceneControl : MonoBehaviour
{
    public GameObject player;
    public GameObject Ground;

    private PathRise PathRise;

    bool playerInit = false;
    void Start()
    {
        player.GetComponent<PathFollower>().enabled = false;
        player.GetComponent<AutoJump>().enabled = false;

        PathRise = Ground.GetComponent<PathRise>();
    }

    void initPlayer()
    {
        if (PathRise != null)
        {
            if (PathRise.isItFinished())
            {
                player.GetComponent<PathFollower>().enabled = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerInit) initPlayer();
        if (Input.GetKeyDown(KeyCode.G))
        {
            player.GetComponent<AutoJump>().enabled = true;
            Debug.Log("AutoJump enabled");
        }
    }
}
