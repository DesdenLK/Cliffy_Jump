using System;
using UnityEngine;
using UnityEngine.UI;

public class Distance_Percentage : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject[] miniLevels;
    public Text percentageText;

    private SceneControl sceneControl;
    private GameObject player;

    private float totalDistance = 0;
    private float currentDistance = 0;
    private float percentage = 0;
    private Vector3 lastPosition = new Vector3();

    void Start()
    {
        sceneControl = GameObject.Find("SceneManager").GetComponent<SceneControl>();
        player = sceneControl.getPlayer();
        for (int i = 0; i < miniLevels.Length; i++)
        {
            totalDistance += getTotalDistance(miniLevels[i].transform.Find("PointsPath").gameObject);
            Debug.Log("Level " + i + " Distance: " + totalDistance);
        }
        lastPosition = player.transform.position;
    }

    public void resetDistance()
    {
        currentDistance = 0;
        lastPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp1 = new Vector3(lastPosition.x, 0, lastPosition.z);
        Vector3 temp2 = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        currentDistance += Vector3.Distance(temp1, temp2);
        lastPosition = player.transform.position;
        percentage = Math.Min((float)Math.Round((currentDistance / totalDistance) * 100, 1), 100f);
        percentageText.text = "Percentage: " + percentage + "%";
        //Debug.Log("Percentage: " + percentage);
        //Debug.Log("Current Distance: " + currentDistance);
        //Debug.Log("Total Distance: " + totalDistance);
    }

    float getTotalDistance(GameObject parent)
    {
        totalDistance = 0;
        bool first = true;
        Vector3 lastPosition = new Vector3();
        foreach (Transform child in parent.transform)
        {
            if (first)
            {
                first = false;
                lastPosition = child.position;
            }
            else
            {
                Vector3 temp1 = new Vector3(lastPosition.x, 0, lastPosition.z);
                Vector3 temp2 = new Vector3(child.position.x, 0, child.position.z);
                totalDistance += Vector3.Distance(temp1, temp2);
                lastPosition = child.position;
            }
        }
        return totalDistance;
    }

    public float getPercentage()
    {
        return percentage;
    }

    public void setPlayerPosition(Transform playerTrans)
    {
        lastPosition = playerTrans.position;
    }
}
