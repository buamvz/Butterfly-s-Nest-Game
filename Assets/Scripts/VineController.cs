using UnityEngine;

public class VineController : MonoBehaviour
{
    [SerializeField] private Animator vineAnimator;
    [SerializeField] private string shakeTrigger = "Shake";
    [SerializeField] private string fadeTrigger = "Fade";


    [SerializeField] private int clicksNeeded = 3;

    private int currentClicks = 0;
    private bool isAnimating = false;
    private bool isFading = false;

    private void OnMouseDown()
    {
        if (isAnimating || isFading)
            return;

        currentClicks++;
        Debug.Log("cutting vine");

        if (currentClicks < clicksNeeded)
        {
            vineAnimator.SetTrigger(shakeTrigger);
            isAnimating = true;
        }
        else
        {
            vineAnimator.SetTrigger(fadeTrigger);
            isFading = true;
        }
    }

    public void OnShakeComplte()
    {
        isAnimating = false;

        if (currentClicks >= clicksNeeded && !isFading)
        {
            // start fading animation
            vineAnimator.SetTrigger(fadeTrigger);
            isFading = true;
            
        }
    }


    public void OnFadeComplete()
    {
        Destroy(gameObject);
        Debug.Log("vine cleared");
    }
}
