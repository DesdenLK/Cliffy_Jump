using UnityEngine;

public class PathRise : MonoBehaviour
{
    public float riseSpeed = 4f;
    public float riseHeight = 0f;
    public bool isRising = true;
    public bool isFinished = false;


    void Start()
    {
        transform.position = new Vector3(transform.position.x, -12, transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isRising && transform.position.y < riseHeight)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + riseSpeed * Time.fixedDeltaTime, transform.position.z);
        }

        if (transform.position.y > riseHeight)
        {
            isRising = false;
        }
        if (!isRising && transform.position.y > riseHeight)
        {
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            isFinished = true;
        }
    }

    public bool isItFinished()
    {
        return isFinished;
    }
}
