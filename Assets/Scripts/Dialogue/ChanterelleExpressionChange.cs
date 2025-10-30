using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChanterelleExpressionChange : MonoBehaviour
{
    [SerializeField] private Dialogue dialogueScript;
    [SerializeField] private Animator anim;

    public List<int> neutralLines = new List<int>();
    public List<int> angryLines = new List<int>();
    public List<int> eyesClosedLines = new List<int>();
    public List<int> smileLines = new List<int>();
    public List<int> meetingLines = new List<int>();
    private void Update()
    {
        int currentIndex = dialogueScript.index;

        // reset all expression
        ResetAllExpressions();

        if (neutralLines.Contains(currentIndex))
        {
            anim.SetBool("neutral", true);
        }
        else if (angryLines.Contains(currentIndex))
        {
            anim.SetBool("angry", true);
        }
        else if (eyesClosedLines.Contains(currentIndex))
        {
            anim.SetBool("eyesClosed", true);
        }
        else if (smileLines.Contains(currentIndex))
        {
            anim.SetBool("smile", true);
        }
        else if (meetingLines.Contains(currentIndex))
        {
            anim.SetBool("meeting", true);
        }
    }

    private void ResetAllExpressions()
    {
        anim.SetBool("neutral", false);
        anim.SetBool("angry", false);
        anim.SetBool("eyesClosed", false);
        anim.SetBool("smile", false);
        anim.SetBool("meeting", false);
    }
}

