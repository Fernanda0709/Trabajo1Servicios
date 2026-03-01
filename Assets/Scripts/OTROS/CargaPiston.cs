using UnityEngine;

public class CargaPiston : MonoBehaviour
{
    public GameObject PosCarga;
    private Vector3 posInicial;
    public Rigidbody piston;
    public float speed = 40f;

    private bool cargando = false;
    private bool descargando = false;

    void Start()
    {
        posInicial = piston.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            cargando = true;
            descargando = false;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            cargando = false;
            descargando = true;
        }
    }

    void FixedUpdate()
    {
        if (cargando)
        {
            piston.MovePosition(Vector3.MoveTowards(piston.position, PosCarga.transform.position, speed * Time.fixedDeltaTime));
        }
        else if (descargando)
        {
            piston.MovePosition(Vector3.MoveTowards(piston.position, posInicial, speed * Time.fixedDeltaTime));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (descargando && other.CompareTag("Ball"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direccion = (posInicial - PosCarga.transform.position).normalized;
                rb.AddForce(direccion * 100f, ForceMode.Impulse);
            }
        }
    }
}
