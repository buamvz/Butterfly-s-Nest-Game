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

    public bool currentState;
    public bool endState;

    public void ToggleActive(bool show)
    {
        colliderToToggle.enabled = show;
        currentState = show;
    }
    IEnumerator Toggle()
    {
        currentState = !currentState;
        Debug.Log("swap visibility");
        yield return new WaitForSeconds(lineTime);
        ToggleActive(currentState);
    }

    private void Awake()
    {
        ToggleActive(currentState);
    }

    // Update is called once per frame
    void Update()
    {

        if (currentState != endState && dialogueScript.index == triggerLine)
        {
            StartCoroutine(Toggle());
        }


    }
}
