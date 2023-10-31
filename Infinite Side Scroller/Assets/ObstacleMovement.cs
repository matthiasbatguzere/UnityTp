using UnityEngine;
using System.Collections;

public class ObstacleMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float appearTime = 2.0f;
    public float disappearTime = 5.0f;

    void Start()
    {
        StartCoroutine(ObstacleLifeCycle());
    }

    IEnumerator ObstacleLifeCycle()
    {
        yield return new WaitForSeconds(appearTime);
        gameObject.SetActive(true);

        yield return new WaitForSeconds(disappearTime - appearTime);
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (gameObject.activeSelf)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
