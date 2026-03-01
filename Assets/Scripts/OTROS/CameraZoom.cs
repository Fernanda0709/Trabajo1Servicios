using System.Collections;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public RectTransform imageTransform; // Asigna el RectTransform de la imagen en el Inspector
    public float zoomFactor = 1.5f; // Tamaño al que se expandirá (1.5 significa 150% del tamaño original)
    public float zoomDuration = 0.5f; // Tiempo que dura el zoom
    public float delayBeforeZoomOut = 1f; // Tiempo que espera antes de volver al tamaño original
    public float loopDelay = 0f; // Tiempo de espera antes de repetir el zoom

    private Vector3 originalScale;

    void Start()
    {
        originalScale = imageTransform.localScale;
        StartCoroutine(ZoomLoop()); // Iniciar el loop infinito
    }

    IEnumerator ZoomLoop()
    {
        while (true) // Bucle infinito
        {
            yield return StartCoroutine(ZoomEffect(originalScale * zoomFactor, zoomDuration)); // Zoom in
            yield return new WaitForSeconds(delayBeforeZoomOut); // Mantiene el zoom por un tiempo
            yield return StartCoroutine(ZoomEffect(originalScale, zoomDuration)); // Zoom out
            yield return new WaitForSeconds(loopDelay); // Espera antes de repetir
        }
    }

    IEnumerator ZoomEffect(Vector3 targetScale, float duration)
    {
        Vector3 startScale = imageTransform.localScale;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            imageTransform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        imageTransform.localScale = targetScale;
    }
}