using UnityEngine;
using System;

public class PuzzleAreaChecker : MonoBehaviour
{
    private int bugsInArea = 0;
    public static event Action OnWebCleared; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bug"))
        {
            bugsInArea++;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Bug"))
        {
            bugsInArea--;

            if (bugsInArea <= 0)
            {
                bugsInArea = 0;
                Debug.Log("web is all cleared");
                OnWebCleared?.Invoke();
            }
        }
    }
}