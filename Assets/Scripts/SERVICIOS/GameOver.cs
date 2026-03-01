using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOver : MonoBehaviour
{
    public string nombreEscena;

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.instancia != null)
            GameManager.instancia.NotificarGameOver();

        SFXManager.instance.PlayGameOver();
        StartCoroutine(CargarEscena());
    }

    private IEnumerator CargarEscena()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(nombreEscena);
    }
}