using UnityEngine;

public class AutoJump : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject player;
    public Transform[] Points;
    private int pointIndex;

    private NormalJump normalJump;
    void Start()
    {
        normalJump = player.GetComponent<NormalJump>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (pointIndex >= Points.Length)
        {
            return;
        }
        float distance = Vector3.Distance(transform.position, Points[pointIndex].transform.position);
        //Debug.Log("Distance: " + distance);
        if (Vector3.Distance(transform.position, Points[pointIndex].transform.position) < 0.1f)
        {
            Debug.Log("Jumping");
            normalJump.Jump();
            pointIndex++;
        }
    }
}
