using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Ajusta la masa y el drag para un comportamiento de PinBall
        rb.mass = 0.5f;
        rb.linearDamping = 0f;
        rb.angularDamping = 0.05f;
    }

    void Update()
    {
        // Control opcional para reiniciar la bola si sale del área de juego
        if (transform.position.y < -10f)
        {
            ResetBall();
        }
    }

    private void ResetBall()
    {
        // Reinicia la posición y velocidad de la bola
        transform.position = Vector3.zero;
        rb.linearVelocity = Vector3.zero;
    }
}