using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{   

    [Header("Nombre de la escena del menú")]
    [SerializeField] private string menuSceneName = "MenuScene";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Time.timeScale = 1f; // asegurar tiempo normal antes de cambiar de escena
            SceneManager.LoadScene(menuSceneName);
        }
    }
}

