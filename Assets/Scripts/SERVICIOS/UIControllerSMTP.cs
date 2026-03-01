using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIControllerSMTP : MonoBehaviour
{
    [Header("Panel completo")]
    [SerializeField] private GameObject panelSMTP;

    [Header("UI")]
    [SerializeField] private TMP_InputField inputFieldEmail;
    [SerializeField] private TMP_Text txtEstado;
    [SerializeField] private Button btnJugar;

    private readonly Color COLOR_OK    = new Color(0.15f, 0.75f, 0.15f);
    private readonly Color COLOR_ERROR = new Color(0.85f, 0.15f, 0.15f);
    private readonly Color COLOR_IDLE  = new Color(0.85f, 0.85f, 0.85f);

    private void OnEnable()
    {
        EmailSender.OnEmailSuccess += MostrarExito;
        EmailSender.OnEmailError   += MostrarError;

        if (btnJugar != null)
            btnJugar.onClick.AddListener(OnClickJugar);

        if (inputFieldEmail != null)
            inputFieldEmail.onValueChanged.AddListener(ActualizarEmail);
    }

    private void OnDisable()
    {
        EmailSender.OnEmailSuccess -= MostrarExito;
        EmailSender.OnEmailError   -= MostrarError;

        if (btnJugar != null)
            btnJugar.onClick.RemoveListener(OnClickJugar);

        if (inputFieldEmail != null)
            inputFieldEmail.onValueChanged.RemoveListener(ActualizarEmail);
    }

    private void Start()
    {
        SetEstado("Ingresa tu correo para recibir el resumen", COLOR_IDLE);
        panelSMTP.SetActive(true);
    }

    private void ActualizarEmail(string valor)
    {
        if (GameManager.instancia != null)
            GameManager.instancia.emailDestino = valor.Trim();
    }

    private void OnClickJugar()
    {
        string email = inputFieldEmail.text.Trim();

        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
        {
            SetEstado("Ingresa un correo valido para continuar", COLOR_ERROR);
            return;
        }

        if (GameManager.instancia != null)
            GameManager.instancia.emailDestino = email;

        panelSMTP.SetActive(false);
    }

    private void MostrarExito(string mensaje)
    {
        panelSMTP.SetActive(true);
        SetEstado(mensaje, COLOR_OK);
    }

    private void MostrarError(string mensaje)
    {
        panelSMTP.SetActive(true);
        SetEstado(mensaje, COLOR_ERROR);
    }

    private void SetEstado(string texto, Color color)
    {
        if (txtEstado == null) return;
        txtEstado.text  = texto;
        txtEstado.color = color;
    }
}