//using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.Playables;

public class DialoguePlayable : PlayableBehaviour
{
    public Dialogue manager;
    //public Dialogue speaker;
    public int startIndex;
    public int endIndex;

    private bool started = false;
    private bool waiting = false;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (!started)
        {
            manager.StartDialogueRange(startIndex, endIndex);
            started = true;
            waiting = true;
        }

        //pause timeline for dialogue
        if (waiting && manager.IsDialogueActive)
        {
            playable.GetGraph().GetRootPlayable(0).SetSpeed(0f);
        }
        //resume timeline when done
        else if (waiting && !manager.IsDialogueActive)
        {
            playable.GetGraph().GetRootPlayable(0).SetSpeed(1);
            waiting = false;
        }

    }

}
