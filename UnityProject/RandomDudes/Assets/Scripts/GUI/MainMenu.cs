using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
    public RectTransform MainMenuGUI;
    public RectTransform CreditsGUI;

    public void Back()
    {
        StartCoroutine(DelayedSetActiv(MainMenuGUI.gameObject, true));
        StartCoroutine(DelayedSetActiv(CreditsGUI.gameObject, false));
    }

    public void StartGame()
    {
        Application.LoadLevel("Intro");
    }

    public void ShowCredits()
    {
        StartCoroutine(DelayedSetActiv(MainMenuGUI.gameObject, false));
        StartCoroutine(DelayedSetActiv(CreditsGUI.gameObject, true));
    }

    public void Quit()
    {
        Application.Quit();
    }


    private IEnumerator DelayedSetActiv(GameObject obj, bool b)
    {
        yield return new WaitForSeconds(0.5f);
        obj.SetActive(b);
    }
    
}
