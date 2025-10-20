using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueToggle : MonoBehaviour
{
    //toggles stuff once a dialogue is done

    [SerializeField] List<BoxCollider2D> collidersToDisable;

    [SerializeField] Dialogue dialogueScript;
    [SerializeField] DialogueTrigger triggerScript;

    [SerializeField] private int triggerLine;
    [SerializeField] private float lineTime;

    [SerializeField] private bool changeOnce;

    private bool hasToggled = false;

    private int lastDialogueIndex = -1;


    //flips completely
    public void ToggleActive()
    {
        foreach (BoxCollider2D collider in collidersToDisable)
        {
            collider.enabled = !collider.enabled;
        }
    }
    IEnumerator Toggle()
    {
        yield return new WaitForSeconds(lineTime);
        ToggleActive();

        Debug.Log("swap visibility");

    }


    // Update is called once per frame
    void Update()
    {
        //triggerLine = triggerScript.dialogueEndLine;

        //old code for only on/off once
        //if (currentState != endState && dialogueScript.index == triggerLine)
        //{
        //    StartCoroutine(Toggle());
        //}

        if (dialogueScript.index != lastDialogueIndex)
        {
            lastDialogueIndex = dialogueScript.index;

            if(dialogueScript.index == triggerLine)
            {
                if(changeOnce)
                {
                    if(!hasToggled)
                    {
                        StartCoroutine(Toggle());
                        hasToggled = true;
                    }
                }
                else
                {
                    StartCoroutine(Toggle());
                }
            }
        }

    }
}
