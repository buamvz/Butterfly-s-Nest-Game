using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    [SerializeField] private bool lightIsOn;
    [SerializeField] private Light2D spotlight;
    [SerializeField] public int lightIndex;

    private LightPuzzleManager manager;

    public void Initialize(LightPuzzleManager puzzleManager, int index)
    {
        manager = puzzleManager;
        lightIndex = index;
        UpdateLightVisual();
    }


    private void OnMouseDown()
    {
        manager?.LightClicked(lightIndex);
    }

    public void ToggleLight()
    {
        lightIsOn = !lightIsOn;
        UpdateLightVisual();
    }

    public void SetLight(bool value)
    {
        lightIsOn = value;
        UpdateLightVisual();
    }

    public bool isLightOn() => lightIsOn;

    private void UpdateLightVisual()
    {
        if (spotlight != null)
        {
            spotlight.enabled = lightIsOn;
        }
    }
}
