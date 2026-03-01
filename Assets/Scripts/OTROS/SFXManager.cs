using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioClip gameOverSound;

    public static SFXManager instance;
    private AudioSource audioSource;

    public AudioClip clickSound;
    public AudioClip collisionSound;

    public void PlayGameOver()
    {
        audioSource.PlayOneShot(gameOverSound);
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayClick()
    {
        audioSource.PlayOneShot(clickSound);
    }

    public void PlayCollision()
    {
        audioSource.PlayOneShot(collisionSound);
    }
}
