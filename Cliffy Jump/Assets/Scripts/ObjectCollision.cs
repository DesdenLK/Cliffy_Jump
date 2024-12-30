using UnityEngine;
using UnityEngine.Rendering;

public class ObjectCollision : MonoBehaviour
{
    public GameObject SceneManager;
    private SceneControl SceneControl;
    private Distance_Percentage Distance_Percentage;
    public ParticleSystem particles;
    public Animator Animator;

    private PathFollower PathFollower;

    private bool particlesPlayed = false;


    void Start()
    {
        PathFollower = GetComponent<PathFollower>();
        SceneControl = SceneManager.GetComponent<SceneControl>();
        Distance_Percentage = SceneManager.GetComponent<Distance_Percentage>();
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
            PathFollower.enabled = false;
            float percentage = Distance_Percentage.getPercentage();
            Debug.Log("Percentage: " + percentage);
            if (percentage > SceneControl.getPercentage())
            {
                SceneControl.setPercentage(percentage);
            }
            PlayerPrefs.SetFloat("percentage", percentage);
            if (particles != null)
            {
                particles.Play();
                particlesPlayed = true;
            }
            if (Animator != null)
            {
                Animator.SetTrigger("Destroy");
            }
            //UnityEngine.SceneManagement.SceneManager.LoadScene("LevelFailed");

        }
    }

}
