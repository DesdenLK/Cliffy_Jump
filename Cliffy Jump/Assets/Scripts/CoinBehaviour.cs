using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    //private GameObject coin;
    public GameObject star;
    private Animator starAnimator;
    private Vector3 rotationAxis;
    private float rotationSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rotationSpeed = 100f;
        rotationAxis = new Vector3(0f, 0f, 1f);
        //Transform starTransform = gameObject.transform.Find("Star");
        if (star != null) 
        {
            starAnimator = star.GetComponent<Animator>();
        }
        else 
        {
            Debug.Log("Star not found as coin child");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationAxis*rotationSpeed*Time.deltaTime);
    }

    void OnTriggerEnter(Collider collision) {
        if (collision.tag == "Player") 
        {
            starAnimator.SetTrigger("StartAnimation");
            star.transform.SetParent(null);
            Destroy(gameObject);
        }
    }
}
