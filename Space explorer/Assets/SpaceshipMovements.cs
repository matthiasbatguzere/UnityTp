using UnityEngine;

public class SpaceshipMovements : MonoBehaviour
{
    public float speed = 1f; // Vitesse de d�placement
    public float speedRotation = 50f; // Vitesse de rotation

    void Update()
    {
        float rotateDirection = Input.GetAxis("Horizontal"); // Utiliser Q et D pour la rotation
        float moveDirection = Input.GetAxis("Vertical"); // Utiliser Z et S pour le d�placement

        // Effectuer la rotation
        transform.Rotate(Vector3.back * rotateDirection * Time.deltaTime * speedRotation);

        // Obtenir la direction dans laquelle la capsule est orient�e
        Vector3 direction = transform.up * moveDirection;

        // Effectuer le d�placement
        //transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }
}
