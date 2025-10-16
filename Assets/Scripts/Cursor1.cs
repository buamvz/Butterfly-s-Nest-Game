using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Cursor1 : MonoBehaviour
{

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite cursorDefault;
    [SerializeField] Sprite cursorClick;
    [SerializeField] Sprite cursorHover;
    [SerializeField] Sprite cursorForward;
    [SerializeField] Sprite cursorLeft;
    [SerializeField] Sprite cursorRight;


    private void Start()
    {
        UnityEngine.Cursor.visible = false;

    }

    public void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(cursorPos.x, cursorPos.y);

        RaycastHit2D hit = Physics2D.Raycast(cursorPos, Vector2.zero);

        //checking hover?
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                spriteRenderer.sprite = cursorHover;
            }
            if (hit.collider.CompareTag("GoForward"))
            {
                spriteRenderer.sprite = cursorForward;
            }
            if (hit.collider.CompareTag("GoLeft"))
            {
                spriteRenderer.sprite = cursorLeft;
            }
            if (hit.collider.CompareTag("GoRight"))
            {
                spriteRenderer.sprite = cursorRight;
            }
            else if(Input.GetMouseButtonUp(0)) //HELP AH 16/109
                spriteRenderer.sprite = cursorDefault;

            //click sprite
            if (hit.collider.CompareTag("Bug") && Input.GetMouseButton(0))
            {
                spriteRenderer.sprite = cursorClick;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                spriteRenderer.sprite = cursorDefault;
            }
        }
        else
            spriteRenderer.sprite = cursorDefault;




    }


}
