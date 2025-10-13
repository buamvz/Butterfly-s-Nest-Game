using UnityEngine;

public class PointCalculator : MonoBehaviour
{
    public static PointCalculator Instance { get; private set; }

    private int totalPoints = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void AddPrizePoints(Prize prize)
    {
        if (prize == null) return;

        int points = prize.GetScore();
        totalPoints += points;

        Debug.Log($"Collected {prize.prizeName} score {points} points. Total: {totalPoints}");
    }

    public int GetTotalPoints()
    {
        return totalPoints;
    }
}