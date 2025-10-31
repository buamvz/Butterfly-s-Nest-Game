using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueToggle : MonoBehaviour
{
    //toggles stuff once a dialogue is done

    [SerializeField] List<BoxCollider2D> collidersToDisable;

    [SerializeField] Dialogue dialogueScript;

    [SerializeField] private int triggerLine;
    //[SerializeField] private float lineTime;

    [SerializeField] private bool changeOnce;

    private bool hasToggled = false;

    private int lastDialogueIndex = -1;

    [SerializeField] private bool triggerEnding = false;

    //flips completely
    public void ToggleActive()
    {
        foreach (BoxCollider2D collider in collidersToDisable)
        {
            collider.enabled = !collider.enabled;
            Debug.Log("toggle active");
        }
    }

    void Update()
    {
        //if (dialogueScript.waiting)
        //    return;

        if (dialogueScript.index != lastDialogueIndex)
        {
            lastDialogueIndex = dialogueScript.index;

            if(dialogueScript.index == triggerLine) 
            {
                if(changeOnce)
                {
                    if(!hasToggled)
                    {
                        ToggleActive();
                        //StartCoroutine(Toggle());
                        hasToggled = true;
                    }
                }
                else
                {
                    ToggleActive();
                    //StartCoroutine(Toggle());
                }

                if (triggerEnding)
                {
                    Debug.Log("[DialougeToggle] ending secisions");
                    EndingDecider.Instance.DecideEnding();
                }

                hasToggled = true;
            }
        }

    }
}
