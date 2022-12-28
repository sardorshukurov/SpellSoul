using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int scoreValue;
    private int points = 20;

    public TextMeshProUGUI scoreText;

    private void Start()
    {
        scoreValue = 0;
    }
    // Update is called once per frame
    void Update()
    {
        scoreText.text = scoreValue.ToString();
    }

    public void AddScore()
    {
        scoreValue += Random.Range(points, points * 5);
    }
}
