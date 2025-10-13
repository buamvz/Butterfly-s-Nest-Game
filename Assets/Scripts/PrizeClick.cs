using UnityEngine;
using UnityEngine.SceneManagement;

public class PrizeClick : MonoBehaviour
{
    private bool isClickable = false;
    private bool alreadyPicked = false;
    private Prize prize;

    private SpriteRenderer spriteRenderer;
    private static bool itemChosen = false; // ensures player can only pick one item

    void Start()
    {
        prize = GetComponent<Prize>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        PuzzleAreaChecker.OnWebCleared += EnableClicking;
    }

    void OnDestroy()
    {
        PuzzleAreaChecker.OnWebCleared -= EnableClicking;
    }

    void EnableClicking()
    {
        isClickable = true;
    }

    void OnMouseEnter()
    {
        if (isClickable && !itemChosen)
            spriteRenderer.material.SetFloat("_OutlineWidth", 2f); 
    }

    void OnMouseExit()
    {
        if (isClickable)
            spriteRenderer.material.SetFloat("_OutlineWidth", 0f);
    }

    void OnMouseDown()
    {
        if (!isClickable || itemChosen || alreadyPicked)
            return;

        alreadyPicked = true;
        itemChosen = true;

        Debug.Log($"{gameObject.name} selected, players score: {prize.score}");

        //add score into calc
        PointCalculator.Instance.AddScore(prize.score);

        ClosePuzzle();
    }

    void ClosePuzzle()
    {
        Time.timeScale = 1f;
        SceneManager.UnloadSceneAsync("Puzzle1_Web");
        Debug.Log("puzzle closed, Scene2 reactivated.");
    }
}