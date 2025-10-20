using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VinesAreaChecker : MonoBehaviour
{
    private int vinesInArea = 0;
    public static event Action OnWebCleared;

    public bool allItemsInspected;

    //[SerializeField] PrizeClick handClick;

    //[SerializeField] Dialogue dialogue;

    public bool webCleared;

    //to turn on and off the prize colliders
    [SerializeField] List<PolygonCollider2D> collidersToDisable;

    //prize colliders off when started
    private void Awake()
    {
        ToggleColliders(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Vine"))
        {
            vinesInArea++;
        }
    }

    private void ToggleColliders(bool show)
    {
        foreach (PolygonCollider2D collider in collidersToDisable)
        {
            if (collider != null)
                collider.enabled = show;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Vine"))
        {
            vinesInArea--;

            if (vinesInArea <= 0)
            {
                vinesInArea = 0;
                Debug.Log("door is all cleared");
                ToggleColliders(true);
                OnWebCleared?.Invoke();

                webCleared = true;
            }
        }
    }

//    void Update()
//    {
//        if (!allItemsInspected && handClick.alreadyInspected && flowerClick.alreadyInspected && goldClick.alreadyInspected)
//        {
//            Debug.Log("all items inspected");
//            allItemsInspected = true;
//            //AllItemsInspected?.Invoke(); 
//        }
//        else
//            return;
//    }
}