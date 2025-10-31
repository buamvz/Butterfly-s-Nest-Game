using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

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
        if (step == null)
        {
            Debug.LogError($"Step {currentStep} is null!");
            return;
        }

        if (step.speaker == null)
        {
            Debug.LogError($"Step {currentStep} has no speaker assigned!");
            return;
        }

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
        Debug.Log($"Waiting to advance from step {currentStep}...");
        yield return new WaitForSeconds(trasitionDelay);

        currentStep++;
        Debug.Log($"Advancing to step {currentStep} / total {steps.Count}");

        if (currentStep < steps.Count)
            StartStep(steps[currentStep]);
        else
            conversationActive = false;

        waitingForNext = false;
    }

    public bool IsDialogueActive
    {
        get
        {
            if(!conversationActive || currentStep >= steps.Count)
                return false;
            return steps[currentStep].speaker.started;
        }
    }

    //public void StartDialogueRange(Dialogue speaker, int start, int end)
    //{
    //    speaker.indexStart = start;
    //    speaker.indexEnd = end;
    //    speaker.StartDialogue();
    //}

}
