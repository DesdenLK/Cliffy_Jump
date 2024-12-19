using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    public GameObject SceneManager;
    private SceneControl SceneControl;

    private void Start()
    {
        SceneControl = SceneManager.GetComponent<SceneControl>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Verifica si el objeto tiene el tag "Object"
        if (collision.gameObject.CompareTag("Object"))
        {
            SceneControl.Reset();
        }
    }
}
