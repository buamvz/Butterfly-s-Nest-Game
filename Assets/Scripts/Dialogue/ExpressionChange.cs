using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpressionChange : MonoBehaviour
{
    [SerializeField] Dialogue dialogueScript;
    [SerializeField] Animator anim;

    public List<int> expressionLine;

    private void Update()
    {
        if (dialogueScript.index == dialogueScript.dialogues.Count && !dialogueScript.waiting)
        {
            anim.SetBool("squint", false);
        }
        else if (expressionLine.Contains(dialogueScript.index))
            anim.SetBool("squint", true);
        else
            anim.SetBool("squint", false);
        

    }
}
