using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI textoPuntaje;

    void Start()
    {
        if (textoPuntaje == null)
            textoPuntaje = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (textoPuntaje == null || GameManager.instancia == null) return;
        textoPuntaje.text = "Puntaje: " + GameManager.instancia.puntos.ToString();
    }
}