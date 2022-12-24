using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    public static Score Instance;
    public int score;
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = score.ToString();
        
    }
    public void AddPoints(int scoreAmount)
    {
        score += scoreAmount;
        scoreText.text = score.ToString();

    }
}
