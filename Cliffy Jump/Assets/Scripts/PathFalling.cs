using UnityEngine;

public class PathFalling : MonoBehaviour
{
    public float fallSpeed = 2f;
    public float fallHeight = -7f;
    public bool isFalling = true;
    public bool isFinished = false;


    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isFalling && transform.position.y > fallHeight)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - fallSpeed * Time.fixedDeltaTime, transform.position.z);
        }

        if (transform.position.y <= fallHeight)
        {
            isFalling = false;
            isFinished = true;
        }
    }

    public bool isItFinished()
    {
        return isFinished;
    }
}
