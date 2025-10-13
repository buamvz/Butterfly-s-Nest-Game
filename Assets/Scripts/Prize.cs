using UnityEngine;

public class Prize : MonoBehaviour
{
    [Header("Prize score")]
    [Tooltip("0 = Bad, 1 = Normal, 2 = Good")]
    public int score; // set this in the Inspector

    public string prizeName;

    public int GetScore()
    {
        return score;
    }
}