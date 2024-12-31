using UnityEngine;


public class PathFollower : MonoBehaviour
{
    private ArcadeJump arcadeJump;

    public Transform[] Points;

    public float moveSpeed;

    private int pointIndex;

    public bool playerFinished = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        arcadeJump = GetComponent<ArcadeJump>();
        Physics.gravity = new Vector3(0, -9.81f, 0);
        pointIndex = 0;
        if (Points.Length > 0)
            transform.position = Points[pointIndex].transform.position;
    }

 
    void Update()
    { 
        
        if (pointIndex < Points.Length)
        {
            bool isFlipping = arcadeJump.IsFrontFlipping;
            Vector3 targetPosition = new Vector3(Points[pointIndex].transform.position.x, transform.position.y, Points[pointIndex].transform.position.z);

            // Look at the moving direction of the player
            Vector3 relativePos = targetPosition - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos, new Vector3(0f, 1f, 0f));
            if (!isFlipping) transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 40f*Time.deltaTime);
            // Move towards next point
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (transform.position.x == Points[pointIndex].transform.position.x && transform.position.z == Points[pointIndex].transform.position.z)
            {
                pointIndex++;
            }

            if (pointIndex == Points.Length)
            {
                playerFinished = true;
            }
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
