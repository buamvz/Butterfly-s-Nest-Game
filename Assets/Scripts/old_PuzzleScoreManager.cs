using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PuzzleScoreManager : MonoBehaviour
{
    public static PuzzleScoreManager Instance { get; private set; }

    private int totalScore = 0;
    private List<ItemSelector> allSelectors = new List<ItemSelector>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        //still have no clue why this is crossed otu but it works??
        allSelectors.AddRange(FindObjectsOfType<ItemSelector>());
    }

    public void AddScore(int value)
    {
        totalScore += value;
        Debug.Log($"puzzle score: {totalScore}");
    }

    public void DisableAllItemSelectors()
    {
        foreach (var selector in allSelectors)
        {
            selector.enabled = false;
        }
    }

    public int GetScore()
    {
        return totalScore;
    }
}