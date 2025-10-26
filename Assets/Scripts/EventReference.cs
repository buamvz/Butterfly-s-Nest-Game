using UnityEngine;

public class EventReference : MonoBehaviour
{

    [SerializeField] DirectionPuzzleManager functionScript;

    public void AnimationEnd()
    {
        functionScript.CheckDirection();
    }

}
