using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public Text scoreText;

    void Start()
    {
        StartCoroutine(IncreaseScoreCoroutine());
    }

    IEnumerator IncreaseScoreCoroutine()
    {
        int increment = 1;
        while (true)
        {
            for (int i = 0; i < 10; i++)
            {
                score += increment;
                UpdateScoreText();
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
