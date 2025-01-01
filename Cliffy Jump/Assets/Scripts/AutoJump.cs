using System;
using UnityEngine;

public class AutoJump : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject player;
    public Transform[] Points;
    private int pointIndex;
    private bool autojump = false;

    private ArcadeJump arcadeJump;
    void Start()
    {
        arcadeJump = player.GetComponent<ArcadeJump>();
        pointIndex = 0;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (pointIndex >= Points.Length)
        {
            return;
        }
        Vector3 playerPos = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        Vector3 pointPos = new Vector3(Points[pointIndex].position.x, 0, Points[pointIndex].position.z);
        float distance = Vector3.Distance(playerPos, pointPos);
        //Debug.Log("Distance: " + distance);
        if (distance <= 0.3f)
        {
            if (autojump)
            {
                arcadeJump.Jump();
            }
            pointIndex++;
        }
    }

    public void initAutoJump()
    {
        pointIndex = 0;
    }

    private int getClosestPoint()
    {
        float minDistance = Mathf.Infinity;
        int index = 0;
        for (int i = 0; i < Points.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, Points[i].transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                index = i;
            }
        }
        return index;
    }

    public void setAutoJump(bool value)
    {
        autojump = value;
    }

    public bool getAutoJump()
    {
        return autojump;
    }
}
