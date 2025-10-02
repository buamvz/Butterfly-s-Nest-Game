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

    private int charIndex;
    public float writingSpeed;

    private bool started;
    public bool waitForNext;

    public bool waiting;

    void Start()
    {
        ToggleWindow(false);
    }



    private void ToggleWindow(bool show)
    {
        Debug.Log("show/hide");
        window.SetActive(show);
        text.SetActive(show);

    }

    public void StartDialogue()
    {
        waiting = true;

        charIndex = 0;
        ToggleWindow(true);
        GetDialogue();

        Debug.Log("started");

        if (started)
            return;

        started = true;
    }

    public void GetDialogue()
    {
        //indexStart = i;
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
        yield return StartCoroutine(Writing());
        waiting = false;
    }

    //IEnumerator Writing()
    //{
    //    string currentDialogue = dialogues[indexStart];
    //    dialogueText.text += currentDialogue[charIndex];
    //    charIndex++;

    //    if(charIndex < currentDialogue.Length)
    //    {
    //        yield return new WaitForSeconds(writingSpeed);
    //        StartCoroutine(Writing());

    //    }
    //    else
    //    {
    //        waitForNext = true;
    //    }

    //}

  
    private bool isTyping;
    public IEnumerator Writing()   //chatgpt vr
    {
        // 1) Validate indexStart
        if (indexStart < 0 || indexStart >= dialogues.Count)
        {
            Debug.LogError($"indexStart {indexStart} out of range (0..{dialogues.Count - 1})");
            yield break;
        }

        isTyping = true;
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

        isTyping = false;
        waitForNext = true;
    }
    public void OnClickShowLine() //chatgpt
    {
        // If already typing, either skip to end or ignore the click
        if (isTyping)
        {
            // Option A: skip to end
            StopAllCoroutines();
            dialogueText.text = dialogues[indexStart];
            charIndex = dialogues[indexStart].Length;
            isTyping = false;
            waitForNext = true;
            return;
        }


        if (indexStart < 0 || indexStart >= dialogues.Count)
        {
            Debug.Log("No more dialogue.");
            return;
        }

        StartCoroutine(Writing());
    }

    private void Update()
    {
        if(waiting == true)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (waitForNext)
            {
                waitForNext = false;
                //indexStart++;
                if (indexStart < dialogues.Count)
                {
                    GetDialogue();
                }
                else
                {
                    EndDialogue();

                }
            }

        }
    }



}
