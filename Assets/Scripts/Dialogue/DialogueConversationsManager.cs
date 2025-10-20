using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueConversationsManager : MonoBehaviour
{
    [Header("Conversation Sequence")]
    [SerializeField] private List<ConversationStep> steps = new List<ConversationStep>();
    [SerializeField] private float trasitionDelay = 0.5f;

    private int currentStep = 0;
    private bool conversationActive = false;
    private bool waitingForNext = false;

    private void Update()
    {
        if (!conversationActive || waitingForNext)
            return;

        if (currentStep >= steps.Count)
        {
            conversationActive = false;
            Debug.Log("Conversation done");
            return;
        }

        var step = steps[currentStep];

        if(!step.speaker.started && !step.speaker.waiting)
        {
            StartCoroutine(WaitAndAdvance());
        }
    }


    public void StartConversation()
    {
        if (steps.Count == 0)
            return;

        currentStep = 0;
        conversationActive = true;
        StartStep(steps[currentStep]);

    }

    private void StartStep(ConversationStep step)
    {
        step.speaker.indexStart = step.startLine;
        step.speaker.indexEnd = step.endLine;

        if(step.trigger != null)
        {
            step.trigger.dialogueStartLine = step.startLine;
            step.trigger.dialogueEndLine = step.endLine;
        }

        step.speaker.StartDialogue();

    }

    private IEnumerator WaitAndAdvance()
    {
        waitingForNext = true;
        yield return new WaitForSeconds(trasitionDelay);

        currentStep++;
        if (currentStep < steps.Count)
            StartStep(steps[currentStep]);
        else
            conversationActive = false;

        waitingForNext = false;
    }

}
