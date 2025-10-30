using UnityEngine;

public class ConfirmPath : MonoBehaviour
{
    //[SerializeField] DirectionPuzzleManager manager;

    public bool confirming;
    public bool confirmPath;

    [SerializeField] GameObject yesOrNo;

    public void ShowOptions(bool show)
    {
        yesOrNo.SetActive(show);
    }

    public void YesPath()
    {
        confirmPath = true;
        confirming = false;
        ShowOptions(false);
    }
    public void NoPath()
    {
        confirmPath = false;
        confirming = false;
        ShowOptions(false);
    }

}
