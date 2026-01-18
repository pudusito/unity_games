using UnityEngine;

public class Money : MonoBehaviour
{
    public int MoneyCount;

    public AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioManager.PlaySFX(audioManager.pickupSound);
            Destroy(gameObject);
            Debug.Log("moneda recogida");
        }
    }
}

