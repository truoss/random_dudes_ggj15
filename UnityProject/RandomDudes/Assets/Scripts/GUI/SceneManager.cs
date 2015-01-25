﻿using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour 
{
    public static SceneManager I;
    private int lastLevel = 2;
    public int[] LevelIndices;

    void Awake()
    {
        I = this;
    }

    public void LoadNextLevel()
    {
        Debug.LogWarning("lastLevel: " + lastLevel);
        int value = GetNumber();
        lastLevel = value;
        Application.LoadLevel(value);        
    }

    private int GetNumber()
    {
        int value = LevelIndices[Random.Range(0, LevelIndices.Length)];
        if (value == lastLevel)
            value = GetNumber();

        Debug.LogWarning("GetNumber: " + value);
        return value;
    }

    [ContextMenu("Load Vulcano")]
    public void LoadVulcano()
    {
        Application.LoadLevel(3);
    }

    [ContextMenu("Load Pac")]
    public void LoadPac()
    {
        Application.LoadLevel(2);
    }
}
