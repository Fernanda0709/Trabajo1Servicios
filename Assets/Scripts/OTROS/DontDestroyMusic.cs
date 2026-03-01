using UnityEngine;

public class DontDestroyMusic : MonoBehaviour
{
    private static DontDestroyMusic instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // No destruir al cargar escena nueva
        }
        else
        {
            Destroy(gameObject); // Elimina duplicados si ya existe
        }
    }
}
