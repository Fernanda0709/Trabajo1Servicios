using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int puntosPorGolpe = 1;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            GameManager.instancia.SumarPuntos(puntosPorGolpe);
        }
    }
}
