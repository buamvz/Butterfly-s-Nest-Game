using UnityEngine;
using UnityEngine.UIElements;

public class LightPuzzleManager : MonoBehaviour
{
    [SerializeField] private LightController[] lights;

    [SerializeField] private PuzzleManager puzzleManager;

    private int[,] toggleMatrix = new int[,]
        {
            { 1,1,0,0,0,0,0,0}, //light 0 - (1) toggles 0,1,7
            { 0,1,1,0,0,0,0,0}, //light 1 - (2) toggles 0,1,2
            { 0,0,1,1,0,0,0,0}, //light 2 - (3) toggles 1 & 2
            { 0,0,0,1,1,0,0,0}, //light 3 - (4) toggles 3 & 4
            { 0,0,0,0,1,1,0,0}, //light 4 - (5) toggles 3,4,5
            { 0,0,0,0,0,1,1,0}, //light 5 - (6) toggles 4 & 5
            { 0,0,0,0,0,0,1,1}, //light 6 - (7) toggles 6 & 7
            { 1,0,0,0,0,0,0,1}, //light 7 - (8) toggles 6,7,1
        };

    private void Start()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].Initialize(this, i);
        }

        lights[0].SetLight(true);
        lights[1].SetLight(false);
        lights[2].SetLight(true);
        lights[3].SetLight(false);
        lights[4].SetLight(true);
        lights[5].SetLight(false);
        lights[6].SetLight(false);
        lights[7].SetLight(true);
    }

    public void LightClicked(int index)
    {
        for (int i = 0; i < lights.Length; i++)
        {
            if (toggleMatrix[index, i] == 1 )
            {
                lights[i].ToggleLight();
            }
        }

        if (CheckAllOn() || CheckAllOff())
        {
            Debug.Log("Puzzle complete");

            bool allOn = CheckAllOn();

            if (BackgroundController.Instance != null)
            {
                BackgroundController.Instance.SetBackground(allOn);
            }
            else
            {
                Debug.LogWarning("no BackgroundController instance found");
            }

            if (puzzleManager != null)
            {
                puzzleManager.ClosePuzzle();
            }
        }
    }

    private bool CheckAllOn()
    {
        foreach (var light in lights)
        {
            if (!light.isLightOn()) return false;
        }
        return true;
    }

    private bool CheckAllOff()
    {
        foreach (var light in lights)
        {
            if (light.isLightOn()) return false;
        }
        return true;
    }

}
