using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int puntos;
    public static GameManager instancia;

    [HideInInspector] public string emailDestino = "";

    private EmailSender _emailSender;
    private bool _emailPuntajeEnviado = false;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _emailSender = gameObject.AddComponent<EmailSender>();
        gameObject.AddComponent<MainThreadDispatcher>();
    }

    public void SumarPuntos(int cantidad)
    {
        puntos += cantidad;
        Debug.Log("Puntaje actual: " + puntos);

        if (puntos > 10)
            NotificarPuntajeAlto();
    }

    private void NotificarPuntajeAlto()
    {
        if (_emailPuntajeEnviado) return;
        if (string.IsNullOrWhiteSpace(emailDestino)) return;

        _emailPuntajeEnviado = true;

        string subject = "[Paintball 3D] Vas muy bien! Puntaje: " + puntos + " pts";

        string body = "Felicitaciones!\n\n"
            + "Superaste los 10 puntos!\n\n"
            + "Puntaje actual: " + puntos + " puntos\n\n"
            + "Fecha: " + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "\n\n"
            + "Sigue asi, eres un crack del paintball!";

        _emailSender.SendEmail(emailDestino, subject, body);
        Debug.Log("[GameManager] Email puntaje alto enviado a: " + emailDestino);
    }

    public void NotificarGameOver()
    {
        if (string.IsNullOrWhiteSpace(emailDestino))
        {
            Debug.Log("[GameManager] Sin email - no se envia correo.");
            return;
        }

        string subject = "[Paintball 3D] Game Over! Puntaje final: " + puntos + " pts";

        string body = "Lo sentimos mucho...\n\n"
            + "PERDISTE :(\n\n"
            + "Tu puntaje final fue: " + puntos + " puntos\n\n"
            + "Fecha: " + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "\n\n"
            + "El campo de batalla no fue tuyo hoy. Vuelve a intentarlo!";

        _emailSender.SendEmail(emailDestino, subject, body);
        Debug.Log("[GameManager] Enviando a: " + emailDestino);
    }
}