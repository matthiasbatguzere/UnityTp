using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // La transform�e de l'objet � suivre
    public Vector3 offset; // Le d�calage par rapport � la position de l'objet

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
            transform.LookAt(target.position);
        }
    }
}
