using UnityEngine;

public class Prize : MonoBehaviour
{
    [Header("Prize score")]
    [Tooltip("0 = bad, 1 = normal, 2 = good")]
    public int score = 0; // 0 = bad, 1 = normal, 2 = good

    public string prizeName;

    public int GetScore()
    {
        return score;
    }
}