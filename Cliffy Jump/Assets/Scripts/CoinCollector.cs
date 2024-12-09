using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private int coinsCollected;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coinsCollected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.CompareTag("Coin")) 
        {
            ++coinsCollected;
            Debug.Log("Coins: " + coinsCollected);
        }
    }
}
