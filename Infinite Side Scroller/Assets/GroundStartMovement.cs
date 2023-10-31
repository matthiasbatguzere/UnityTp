using UnityEngine;

public class GroundStartMovement : MonoBehaviour
{
    public float speed = 5.0f; // Vitesse du mouvement du sol.
    public float disappearTime = 5.0f; // Temps avant que le sol disparaisse.
    private float elapsedTime = 0f; // Temps écoulé depuis le début du mouvement.

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= disappearTime)
        {
            Destroy(gameObject); // Détruit l'objet du sol après le temps spécifié.
        }
    }
}
