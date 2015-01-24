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
    public void LoadVulcano()
    {
        Application.LoadLevel(2);
    }

    [ContextMenu("Load Pac")]
    public void LoadPac()
    {
        Application.LoadLevel(1);
    }
}
