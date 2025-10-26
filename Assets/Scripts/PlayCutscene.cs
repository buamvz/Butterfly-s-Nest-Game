using UnityEngine;
using UnityEngine.Playables;

public class PlayCutscene : MonoBehaviour
{

    [SerializeField] PlayableDirector timelineDirector;
    [SerializeField] BoxCollider2D colliderInteraction;


    public void StartCutscene()
    {
        timelineDirector.Play();
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider == colliderInteraction)
            {

                StartCutscene();
            }

        }
    }
}
