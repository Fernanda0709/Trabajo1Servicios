using UnityEngine;

public class BallCollisionSound : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (SFXManager.instance != null)
        {
            SFXManager.instance.PlayCollision();
        }
    }
}
