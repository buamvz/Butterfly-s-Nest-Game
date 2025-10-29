using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzleAreaChecker : MonoBehaviour
{
    private int bugsInArea = 0;
    public static event Action OnWebCleared;


    public bool webCleared;

    //to turn on and off the prize colliders
    [SerializeField] List<BoxCollider2D> collidersToDisable;

    //prize colliders off when started
    private void Awake()
    {
        ToggleColliders(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bug"))
        {
            bugsInArea++;
        }
    }

    private void ToggleColliders(bool show)
    {
        foreach (BoxCollider2D collider in collidersToDisable)
        {
            if (collider != null)
                collider.enabled = show;
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
                ToggleColliders(true);
                OnWebCleared?.Invoke();

                webCleared = true;
            }
        }
    }


}