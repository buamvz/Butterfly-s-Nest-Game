using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Dialogue_old : MonoBehaviour
{
    //note: 11/10/25 try having index end be like - index++ -> if index => index end -> end dialoge

    public GameObject text;
    public TMP_Text dialogueText;


    public List<string> dialogues;

    public int indexStart;

    public int index;

    private int charIndex;
    public float writingSpeed;

    private bool started;
    public bool waitForNext;

    public bool waiting;


    //multi dialogue or not
    public bool multipleDialogues;

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
        waiting = true;

        charIndex = 0;
        ToggleWindow(true);
        if (multipleDialogues == false)
            GetDialogue(indexStart);
        else if (multipleDialogues == true)
            GetDialogue(index);

        Debug.Log("started");

        if (started)
            return;

        started = true;
    }

    public void GetDialogue(int i)
    {
        index = i;
        charIndex = 0;
        dialogueText.text = string.Empty;
        
        //StartCoroutine(Writing());
        StartCoroutine(RunDialogue());

    }

    public void EndDialogue()
    {
        charIndex = 0;
        ToggleWindow(false);

        waiting = false;
    }

    IEnumerator RunDialogue()
    {
        if (multipleDialogues ==  false)
            yield return StartCoroutine(Writing());
        else if (multipleDialogues == true)
            yield return StartCoroutine(WritingMulti());
        waiting = false;
    }

    //for writing multiple lines
    IEnumerator WritingMulti()
    {
        yield return new WaitForSeconds(writingSpeed);

        string currentDialogue = dialogues[index];
        //Write the character
        dialogueText.text += currentDialogue[charIndex];
        //increase the character index 
        charIndex++;
        //Make sure you have reached the end of the sentence
        if (charIndex < currentDialogue.Length)
        {
            //Wait x seconds 
            yield return new WaitForSeconds(writingSpeed);
            //Restart the same process
            StartCoroutine(WritingMulti());
        }
        else
        {
            //End this sentence and wait for the next one
            waitForNext = true;
        }
    }



    public IEnumerator Writing()   //chatgpt vr
    {
        // 1) Validate indexStart
        if (indexStart < 0 || indexStart >= dialogues.Count)
        {
            Debug.LogError($"indexStart {indexStart} out of range (0..{dialogues.Count - 1})");
            yield break;
        }

        waitForNext = false;

        string currentDialogue = dialogues[indexStart];

        // 2) Reset before starting a new line
        dialogueText.text = string.Empty;
        charIndex = 0;

        // 3) Loop instead of recursive coroutines
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
        if(waiting == true)
            return;

    

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider == null)
                return;
                

            if (waitForNext && multipleDialogues == false)
            {
                waitForNext = false;

                if (indexStart < dialogues.Count)
                {
                    GetDialogue(indexStart);
                }
                else
                {
                    EndDialogue();

                }
            }

            if (waitForNext && multipleDialogues == true)
            {
                waitForNext = false;
                index++;
                if (index < dialogues.Count)
                {
                    GetDialogue(index);
                }
                else
                {
                    EndDialogue();

                }
            }

        }
    }



}
