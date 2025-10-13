using System;
using System.Collections;
using UnityEngine;

public class DialogueToggle : MonoBehaviour
{
    //toggles stuff once a dialogue is done
    [SerializeField] BoxCollider2D colliderToToggle;
    [SerializeField] Dialogue dialogueScript;

    [SerializeField] private int triggerLine;
    [SerializeField] private float lineTime;

    private bool objectOn;

    public void ToggleActive(bool show)
    {
        colliderToToggle.enabled = show;
        objectOn = show;
    }
    IEnumerator ShowThing()
    {
        yield return new WaitForSeconds(lineTime);
        ToggleActive(true);
    }

    private void Awake()
    {
        ToggleActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (!objectOn && dialogueScript.index == triggerLine)
        {
            StartCoroutine(ShowThing());
        }

    }
}
