using UnityEngine;

public class DialogueConversations_old : MonoBehaviour
{

    [Header("Character A")]
    [SerializeField] Dialogue characterA; //this character
    [SerializeField] DialogueTrigger characterATrigger; //this character

    //[SerializeField] BoxCollider2D triggerCollider; //trigger of this dialoge

    [Header("Character B")]
    [SerializeField] Dialogue characterB; //other character
    [SerializeField] DialogueTrigger characterBTrigger;

    [Header("Conversation Settings")]
    [SerializeField] private int triggerLine;
    [SerializeField] private bool conversationActive;
    private bool aTurn = false;




    //just for spider
    public bool squint;


    public void StartConversation()
    {
        conversationActive = true;
        aTurn = true;
        StartNextDialogue(characterA, characterATrigger);
    }

    public void StartNextDialogue(Dialogue dialogue, DialogueTrigger trigger)
    {
        dialogue.indexStart = trigger.dialogueStartLine;
        dialogue.indexEnd = trigger.dialogueEndLine;
        dialogue.StartDialogue();
    }



    private void Update()
    {
        if (!conversationActive) return;

        if (aTurn)
        {
            //when a reaches trigger line, switch to b
            if (characterA.index == triggerLine && !characterB.started)
            {
                StartNextDialogue(characterB, characterBTrigger);
                aTurn = false;
            }
        } else
        {
            if (!characterB.started && !characterA.started)
            {
                StartNextDialogue(characterA, characterATrigger);
                aTurn = true;
            }
        }
    }

}
