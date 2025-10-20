using UnityEngine;

[System.Serializable]
public class ConversationStep
{
    public Dialogue speaker;              // Which dialogue component to use
    public DialogueTrigger trigger;       // Controls which lines to play
    public int startLine;                 // Dialogue start index
    public int endLine;                   // Dialogue end index
}
