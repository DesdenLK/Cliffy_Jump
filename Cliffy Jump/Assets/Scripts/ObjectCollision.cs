using UnityEngine;
using UnityEngine.Rendering;

public class ObjectCollision : MonoBehaviour
{
    public GameObject SceneManager;
    private SceneControl SceneControl;
    private Distance_Percentage Distance_Percentage;
    public ParticleSystem particles;
    public Animator Animator;

    private AudioSource deathSound;

    private PathFollower PathFollower;

    private bool particlesPlayed = false;


    void Start()
    {
        PathFollower = GetComponent<PathFollower>();
        SceneControl = SceneManager.GetComponent<SceneControl>();
        Distance_Percentage = SceneManager.GetComponent<Distance_Percentage>();
        deathSound = gameObject.AddComponent<AudioSource>();
        deathSound.clip = Resources.Load<AudioClip>("Sounds/Death");
    }

    void Update()
    {
        if (particles != null && !particles.IsAlive() && particlesPlayed)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("LevelFailed");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica si el objeto tiene el tag "Object"
        if (collision.gameObject.CompareTag("Object"))
        {
            if (deathSound != null)
            {
                deathSound.Play();
            }
            if (particles != null)
            {
                particles.Play();
                particlesPlayed = true;
            }
            if (Animator != null)
            {
                Animator.SetTrigger("Destroy");
            }
            PathFollower.enabled = false;
            float percentage = Distance_Percentage.getPercentage();
            Debug.Log("Percentage: " + percentage);
            if (percentage > SceneControl.getPercentage())
            {
                SceneControl.setPercentage(percentage);
            }
            PlayerPrefs.SetFloat("percentage", percentage);
            
            //UnityEngine.SceneManagement.SceneManager.LoadScene("LevelFailed");

        }
    }

}
