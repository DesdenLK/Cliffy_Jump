using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public Transform[] Points;

    public float moveSpeed;

    private int pointIndex;

    public bool isLooping = false;

    public Rigidbody player;

    //public float jumpForce = 1f;


    //private bool Jumping = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = Points[pointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space) && !Jumping)
        {
            Jumping = true;
            player.AddForce(jumpForce * Vector3.up, ForceMode.VelocityChange);
        }
        else { Jumping = false; }
        */


        Vector3 targetPosition = new Vector3(Points[pointIndex].transform.position.x, transform.position.y, Points[pointIndex].transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (transform.position.x == Points[pointIndex].transform.position.x && transform.position.z == Points[pointIndex].transform.position.z)
        {
            pointIndex++;
        }

        if (pointIndex == Points.Length)
        {
            if (isLooping)
            {
                pointIndex = 0;
            }
        }
    }
}
