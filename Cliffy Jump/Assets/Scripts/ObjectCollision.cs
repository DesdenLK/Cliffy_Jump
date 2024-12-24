using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    public GameObject SceneManager;
    private SceneControl SceneControl;
    private Distance_Percentage Distance_Percentage;


    private void Start()
    {
        SceneControl = SceneManager.GetComponent<SceneControl>();
        Distance_Percentage = SceneManager.GetComponent<Distance_Percentage>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Verifica si el objeto tiene el tag "Object"
        if (collision.gameObject.CompareTag("Object"))
        {
            float percentage = Distance_Percentage.getPercentage();
            Debug.Log("Percentage: " + percentage);
            if (percentage > SceneControl.getPercentage())
            {
                SceneControl.setPercentage(percentage);
            }
            PlayerPrefs.SetFloat("percentage", percentage);
            UnityEngine.SceneManagement.SceneManager.LoadScene("LevelFailed");
        }
    }

}
