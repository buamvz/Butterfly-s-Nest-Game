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
        if (expressionLine.Contains(dialogueScript.index))
            anim.SetBool("squint", true);
        else
            anim.SetBool("squint", false);
        
    }
}
