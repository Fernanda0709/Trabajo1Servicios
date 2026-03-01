using UnityEngine;

public class FliperDerecho : MonoBehaviour
{
    public float velocidad = 20f;

    private Quaternion rotacionObjetivo;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rotacionObjetivo = rb.rotation;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rotacionObjetivo = Quaternion.Euler(6f, 0, 0);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            rotacionObjetivo = Quaternion.Euler(65f, 0, 0);
        }
    }

    void FixedUpdate()
    {
        Quaternion nuevaRotacion = Quaternion.Lerp(rb.rotation, rotacionObjetivo, velocidad * Time.fixedDeltaTime);
        rb.MoveRotation(nuevaRotacion);
    }
}