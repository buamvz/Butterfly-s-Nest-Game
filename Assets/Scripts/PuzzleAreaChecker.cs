using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class PuzzleAreaChecker : MonoBehaviour
{
    private List<GameObject> bugsInArea = new List<GameObject>();
    public GameObject[] itemsToActivate; // teh items Hand, Gold, Flower
    private bool puzzleCleared = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bug") && !bugsInArea.Contains(other.gameObject))
        {
            bugsInArea.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Bug"))
        {
            bugsInArea.Remove(other.gameObject);

            if (bugsInArea.Count == 0 && !puzzleCleared)
            {
                puzzleCleared = true;
                EnableItemSelection();
            }
        }
    }

    private void EnableItemSelection()
    {
        Debug.Log("all bug removed, now can choose an items");
        foreach (GameObject item in itemsToActivate)
        {
            if (item != null)
            {
                ItemSelector selector = item.GetComponent<ItemSelector>();
                if (selector != null)
                    selector.SetSelectable(true);
            }
        }
    }
}