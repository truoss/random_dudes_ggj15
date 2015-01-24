using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour 
{
    public static SceneManager I;

    void Awake()
    {
        I = this;
    }

    [ContextMenu("Load Vulcano")]
    void LoadVulcano()
    {
        Application.LoadLevel(4);
    }

    [ContextMenu("Load Pac")]
    void LoadPac()
    {
        Application.LoadLevel(1);
    }
}
