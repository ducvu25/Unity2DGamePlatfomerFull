using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameValue : MonoBehaviour
{
    public static GameValue Instance { get; private set; }
    public Color[] colorsLevelOfStrength;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }
}
