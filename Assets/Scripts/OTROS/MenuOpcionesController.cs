using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuOpcionesController : MonoBehaviour
{
    public GameObject settings;           // Panel del menú
    public Button btnSettings;            // Botón que abre el menú
    public Button soundToggle;            // Botón para sonidos
    public Button musicToggle;            // Botón para música
    public Button resumeButton;           // Botón de reanudar juego
    public Button homeButton;             // Botón para ir al inicio

    public AudioSource musicSource;       // Fuente de música (asignar en Inspector)

    private bool soundOn = true;
    private bool musicOn = true;

    void Start()
    {
        settings.SetActive(false); // Ocultar panel al inicio

        btnSettings.onClick.AddListener(ToggleMenu);
        soundToggle.onClick.AddListener(ToggleSound);
        musicToggle.onClick.AddListener(ToggleMusic);
        resumeButton.onClick.AddListener(ResumeGame);
        homeButton.onClick.AddListener(GoHome);
    }

    void ToggleMenu()
    {
        bool isActive = settings.activeSelf;
        settings.SetActive(!isActive);
        Time.timeScale = isActive ? 1 : 0; // Si se cierra, reanuda; si se abre, pausa
    }

    void ToggleSound()
    {
        soundOn = !soundOn;
        AudioListener.pause = !soundOn;
        Debug.Log("Sonido: " + (soundOn ? "Activado" : "Desactivado"));
    }

    void ToggleMusic()
    {
        musicOn = !musicOn;
        if (musicSource != null)
        {
            musicSource.mute = !musicOn;
        }
        Debug.Log("Música: " + (musicOn ? "Activada" : "Desactivada"));
    }

    void ResumeGame()
    {
        settings.SetActive(false);
        Time.timeScale = 1;
        Debug.Log("Juego reanudado");
    }

    void GoHome()
    {
        Time.timeScale = 1;
        Debug.Log("Volver al inicio");
        SceneManager.LoadScene("MainMenu"); // Cambia por tu escena de inicio
    }
}
