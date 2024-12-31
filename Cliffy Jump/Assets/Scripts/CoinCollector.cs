using UnityEngine;
using UnityEngine.UI;

public class CoinCollector : MonoBehaviour
{
    private int coinsCollected;
    public Text coinsCollectedText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coinsCollected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (coinsCollectedText != null)
        {
            coinsCollectedText.text = "COINS: " + coinsCollected;
        }
    }

    void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.CompareTag("Coin")) 
        {
            ++coinsCollected;
            Debug.Log("Coins: " + coinsCollected);
        }
    }
}
