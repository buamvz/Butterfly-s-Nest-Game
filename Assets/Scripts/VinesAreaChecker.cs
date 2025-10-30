using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VinesAreaChecker : MonoBehaviour
{
    private int vinesInArea = 0;
    public static event Action OnVinesCleared;

    public bool vinesCleared;

    //[SerializeField] PrizeClick handClick;

    //[SerializeField] Dialogue dialogue;

    [SerializeField] private GameObject prizeScene;

    //to turn on and off the prize colliders
   [SerializeField] private List<Collider2D> collidersToDisable = new List<Collider2D>();

    //prize colliders off when started
    private void Awake()
    {
        if (prizeScene != null)
            prizeScene.SetActive(false);

        ToggleColliders(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Vine"))
        {
            vinesInArea++;
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
                Debug.Log("vines all cleared");

                vinesCleared = true;

                ActivatePrizeScene();
                ToggleColliders(true);
                OnVinesCleared?.Invoke();
            }
        }
    }

    private void ActivatePrizeScene()
    {
        if (prizeScene != null)
        {
            prizeScene.SetActive(true);

            var prizePosition = prizeScene.transform.position;
            prizeScene.transform.position = new Vector3(prizePosition.x, prizePosition.y, -5f);
        }
        else
        {
            Debug.LogWarning("no prize scene assigned");
        }
    }
    private void ToggleColliders(bool show)
    {
        foreach (Collider2D collider in collidersToDisable)
        {
            if (collider != null)
                collider.enabled = show;
        }
    }

}