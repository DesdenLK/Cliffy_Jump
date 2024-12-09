using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    //private GameObject coin;
    private Vector3 rotationAxis;
    private float rotationSpeed;
    public GameObject disappearEffectPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rotationSpeed = 100f;
        rotationAxis = new Vector3(0f, 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationAxis*rotationSpeed*Time.deltaTime);
    }

    void OnTriggerEnter(Collider collision) {
        if (collision.tag == "Player") 
        {
            if (disappearEffectPrefab != null) 
            {
                Instantiate(disappearEffectPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
