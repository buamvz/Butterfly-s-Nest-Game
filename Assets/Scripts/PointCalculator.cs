using UnityEngine;

public class PointCalculator : MonoBehaviour
{
    public static PointCalculator Instance;
    public int totalScore = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddScore(int score)
    {
        totalScore += score;
        Debug.Log($"score added: {score}, players total = {totalScore}");
    }
}