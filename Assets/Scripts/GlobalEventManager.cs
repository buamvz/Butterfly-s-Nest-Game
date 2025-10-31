using System;
using UnityEngine;

public class GlobalEventManager : MonoBehaviour
{

    public static GlobalEventManager Instance;
    public event System.Action OnPuzzleClosed;

    public event Action<int> OnPointsAdded;

    void Awake()
    {
        if (Instance == null) 
        { 
            Instance = this; DontDestroyOnLoad(gameObject); 
        }
        else Destroy(gameObject);
    }

    public void PuzzleClosed() => OnPuzzleClosed?.Invoke();

    public void PrizePointsAdded(int amount)
    {
        OnPointsAdded?.Invoke(amount);
    }

}
