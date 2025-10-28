using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Dialogue : MonoBehaviour
{

    public GameObject text;
    public TMP_Text dialogueText;


    public List<string> dialogues;

    public int indexStart;
    public int indexEnd;

    public int index;

    private int charIndex;
    public float writingSpeed;

    public bool started;
    public bool waitForNext;

    public bool waiting;


    //multi dialogue or not
    //public bool multipleDialogues;

    void Start()
    {
        ToggleWindow(false);
    }



    private void ToggleWindow(bool show)
    {
        text.SetActive(show);

    }

    public void StartDialogue()
    {

        if (started)
            return;

        waiting = true;

        charIndex = 0;
        ToggleWindow(true);

        GetDialogue(indexStart);

        started = true;
    }

    public void GetDialogue(int i)
    {
        index = i;
        charIndex = 0;
        dialogueText.text = string.Empty;
        
        StartCoroutine(Writing());

    }

    public void EndDialogue()
    {
        waiting = false;
        started = false;
        waitForNext = false;

        ToggleWindow(false);

        StopAllCoroutines();
    }




    public IEnumerator Writing()   //chatgpt vr
    {

        yield return new WaitForSeconds(writingSpeed);

        string currentDialogue = dialogues[index];

        charIndex = 0;

        //this works (chatgpt)
        while (charIndex < currentDialogue.Length)
        {
            dialogueText.text += currentDialogue[charIndex];
            charIndex++;
            yield return new WaitForSeconds(writingSpeed);
        }

        waitForNext = true;

    }

    IEnumerator WaitSeconds()
    {
        yield return new WaitForSeconds(1);
    }

    private void Update()
    {
        //if(waiting == true)
        //    return;
        if (!started)
            return;


        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            //if (hit.collider == null)
            //    return;
                

            if (waitForNext)
            {
                waitForNext = false;
                index++;

                if (index <= indexEnd)
                {
                    GetDialogue(index);
                }
                else
                {

                    index--;
                    EndDialogue();
                }
            }


        }
    }

    public bool IsDialogueActive => started;
    public void StartDialogueRange(int start, int end)
    {
        indexStart = start;
        indexEnd = end;
        StartDialogue();
    }


}
