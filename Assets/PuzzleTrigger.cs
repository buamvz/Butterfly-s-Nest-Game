using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    private PuzzleManager puzzleManager;

    void Start()
    {
        //fiund puzzlemanager object idk why the helly its crossed out??
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
            Debug.LogWarning("puzzlemanager not found");
        }
    }
}