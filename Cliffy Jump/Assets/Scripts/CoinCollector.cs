using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CoinCollector : MonoBehaviour
{
    private int coinsCollected;
    public Text coinsCollectedText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private AudioSource coinSound;
    void Start()
    {
        coinSound = gameObject.AddComponent<AudioSource>();
        coinSound.clip = Resources.Load<AudioClip>("Sounds/Coin");
        coinSound.playOnAwake = false;
        coinSound.loop = false;
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
            coinSound.Play();
            ++coinsCollected;
            Debug.Log("Coins: " + coinsCollected);
        }
    }
}
