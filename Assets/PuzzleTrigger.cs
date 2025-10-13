using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    private PuzzleManager puzzleManager;

    void Start()
    {
        //finding puzzle managaer
        puzzleManager = FindObjectOfType<PuzzleManager>();
    }

    private void OnMouseDown()
    {
        if (puzzleManager != null)
        {
            puzzleManager.OpenPuzzle();
        }
        else
        {
            Debug.LogWarning("PuzzleManager not found in scene!");
        }
    }
}