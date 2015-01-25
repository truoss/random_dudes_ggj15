using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour 
{
    public static SceneManager I;
    private static int lastLevel = 1;
    public int[] LevelIndices;

    void Awake()
    {
        I = this;
    }

    public void LoadIntroLevel()
    {
        //.LogWarning("lastLevel: " + lastLevel);
        //int value = GetNumber();
        //lastLevel = value;
        Application.LoadLevel("Intro");        
    }
    
    public void LoadNextLevel()
    {
        //.LogWarning("lastLevel: " + lastLevel);
        int value = GetNumber();
        lastLevel = value;
        Application.LoadLevel(value);        
    }
    

    private int GetNumber()
    {
        /*int value = LevelIndices[Random.Range(0, LevelIndices.Length)];
        if (value == lastLevel)
            value = GetNumber();
        */
        Debug.LogWarning("GetNumber: " + lastLevel);
        int value = lastLevel + 1;
        if (value > 5)
            value = 2;
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
