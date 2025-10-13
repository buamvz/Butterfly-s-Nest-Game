using System.Collections;
using UnityEngine;

public class ApproachSpider : MonoBehaviour
{

    [SerializeField] Dialogue dialogueScript;
    [SerializeField] BoxCollider2D forwardCollider;

    [SerializeField] SceneLoader sceneScript;
    [SerializeField] Animator camAnimation;


    void Update()
    {


        if (Input.GetMouseButtonDown(0) && dialogueScript.waiting == false)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider == forwardCollider)
                WalkForward();
        }
    }


    void WalkForward()
    {
        camAnimation.SetTrigger("Zoom");
        sceneScript.LoadNextScene();
    }
}
