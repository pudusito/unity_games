using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------------------Audio Sources--------------------")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource effectsSource;

    [Header("------------------Audio Clips--------------------")]
    public AudioClip backgroundMusic;
    public AudioClip pickupSound;

    private void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        effectsSource.PlayOneShot(clip);
    }
    
}
