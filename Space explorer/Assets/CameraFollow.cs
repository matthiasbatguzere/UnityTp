using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // La transformée de l'objet à suivre
    public Vector3 offset; // Le décalage par rapport à la position de l'objet

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
            transform.LookAt(target.position);
        }
    }
}
