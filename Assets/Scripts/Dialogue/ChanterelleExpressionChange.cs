using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChanterelleExpressionChange : MonoBehaviour
{
    [SerializeField] private Dialogue dialogueScript;
    [SerializeField] private Animator anim;

    //public List<int> neutralLines = new List<int>();
    public List<int> talkingLines = new List<int>();
    public List<int> talking2Lines = new List<int>();
    public List<int> smileLines = new List<int>();
    public List<int> disgustLines = new List<int>();
    public List<int> blushLines = new List<int>();
    private void Update()
    {
        int currentIndex = dialogueScript.index;

        // reset all expression
        ResetAllExpressions();

        //if (neutralLines.Contains(currentIndex))
        //{
        //    anim.SetBool("neutral", true);
        //}
        if (dialogueScript.waiting)
        {
            if (talkingLines.Contains(currentIndex))
            {
                ResetAllExpressions();
                anim.SetBool("talking", true);
            }
            else if (talking2Lines.Contains(currentIndex))
            {
                ResetAllExpressions();
                anim.SetBool("talking2", true);
            }
            else if (smileLines.Contains(currentIndex))
            {
                ResetAllExpressions();
                anim.SetBool("smile", true);
            }
            else if (disgustLines.Contains(currentIndex))
            {
                ResetAllExpressions();
                anim.SetBool("disgust", true);
            }
            else if (blushLines.Contains(currentIndex))
            {
                ResetAllExpressions();
                anim.SetBool("blush", true);
            }
            else
            {
                ResetAllExpressions();
            }
        }

    }

    private void ResetAllExpressions()
    {
        anim.SetBool("talking", false);
        anim.SetBool("talking2", false);
        anim.SetBool("smile", false);
        anim.SetBool("disgust", false);
        anim.SetBool("blush", false);
        //anim.SetBool("meeting", false);
    }
}

