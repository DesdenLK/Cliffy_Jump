using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public Transform[] Points;

    public float moveSpeed;

    private int pointIndex;

    public bool playerFinished = false;

    public Rigidbody player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = Points[pointIndex].transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        if (pointIndex >= Points.Length)
        {
            return;
        }

        Vector3 targetPosition = new Vector3(Points[pointIndex].transform.position.x, transform.position.y, Points[pointIndex].transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.fixedDeltaTime);

        if (transform.position.x == Points[pointIndex].transform.position.x && transform.position.z == Points[pointIndex].transform.position.z)
        {
            pointIndex++;
        }

        if (pointIndex == Points.Length)
        {
            playerFinished = true;
        }
    }

    public bool isPlayerFinished()
    {
        return playerFinished;
    }

    public void setPlayerFinished(bool value)
    {
        playerFinished = value;
    }

    public void initPathFollower()
    {
        pointIndex = 0;
        transform.position = Points[pointIndex].transform.position;
        playerFinished = false;
    }
}
