using UnityEngine;

public class PathFollowerInfinit : MonoBehaviour
{
    public Transform[] Points;

    public float moveSpeed;

    private int pointIndex;

    public bool playerFinished = false;

    public Rigidbody player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pointIndex = 0;
        if (Points.Length > 0)
            transform.position = new Vector3 (Points[pointIndex].transform.position.x, transform.position.y, Points[pointIndex].transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        if (pointIndex >= Points.Length)
        {
            pointIndex = 0;
        }

        Vector3 targetPosition = new Vector3(Points[pointIndex].transform.position.x, transform.position.y, Points[pointIndex].transform.position.z);

        // Look at the moving direction of the player
        Vector3 relativePos = targetPosition - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, new Vector3(0f, 1f, 0f));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 40f*Time.deltaTime);
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
