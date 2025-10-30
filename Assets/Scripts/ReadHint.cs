using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ReadHint : MonoBehaviour
{

    [SerializeField] Dialogue dialogue;

    [SerializeField] BoxCollider2D itemCollider;
    [SerializeField] BoxCollider2D hintUpCollider;

    [SerializeField] List<BoxCollider2D> collidersToDisable;

    [Header("Sprites")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite hoverSprite;

    [SerializeField] GameObject hintPopUp;

    public bool readingHint;

    private void Awake()
    {
        hintPopUp.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!readingHint)
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(cursorPos, Vector2.zero);

            //checking hover?
            if (hit.collider != null)
            {
                if (hit.collider == itemCollider)
                {
                    spriteRenderer.sprite = hoverSprite;

                    if (Input.GetMouseButton(0) && !dialogue.waiting)
                        StartCoroutine(ToggleHintWindow(true));
                }
                else
                {
                    spriteRenderer.sprite = defaultSprite;
                }
           
            }
            else
                spriteRenderer.sprite = defaultSprite;
        }
        else
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(cursorPos, Vector2.zero);
            if (hit.collider == null && Input.GetMouseButton(0))
            {
                dialogue.indexStart = 3;
                dialogue.indexEnd = 3;
                dialogue.StartDialogue();

                StartCoroutine(ToggleHintWindow(false));
            }

        }

    }

    IEnumerator ToggleHintWindow(bool show)
    {
        hintPopUp.SetActive(show);
        readingHint = show;

        spriteRenderer.enabled = !show;

        if(show == false)
            yield return new WaitForSeconds(2);

        ToggleColliders(!show);
    }

    void ToggleColliders(bool show)
    {
        foreach (BoxCollider2D collider in collidersToDisable)
        {
            collider.enabled = show;
        }
    }

}
