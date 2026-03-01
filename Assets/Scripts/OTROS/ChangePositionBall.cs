using UnityEngine;

public class ChangePositionBall : MonoBehaviour
{
    public Transform positionBall;

    public void OnTriggerEnter(Collider other)
    {
        other.transform.position = positionBall.position;
        GameManager.instancia.SumarPuntos(10);
    }
}