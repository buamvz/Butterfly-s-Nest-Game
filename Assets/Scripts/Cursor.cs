using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Cursor : MonoBehaviour
{

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite cursorDefault;
    [SerializeField] Sprite cursorClick;
    [SerializeField] Sprite cursorHover;

    //public HoverCheck hoverScript;
        public bool mouseHover;

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
                Debug.Log("......hover");
                mouseHover = true;
            }


            mouseHover = false;
        }


        //hoversprite
        if (mouseHover == true)
            spriteRenderer.sprite = cursorHover;
        else
            spriteRenderer.sprite = cursorDefault;

        //click sprite
        if (Input.GetMouseButton(0))
            spriteRenderer.sprite = cursorClick;
        else
            spriteRenderer.sprite = cursorDefault;


    }


}
