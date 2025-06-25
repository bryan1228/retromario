using UnityEngine;
using TMPro;  // ← add this

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI")]
    public TextMeshProUGUI scoreText;  
    public int perfectScore = 6;

    private int score = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    void Start()
    {
        score = 0;
        UpdateUI();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (scoreText == null) return;
        scoreText.text = $"Score: {score} / {perfectScore}";
        scoreText.color = (score >= perfectScore) ? Color.green : Color.white;
    }
}
