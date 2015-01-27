using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour {

    public AudioClip question;
    public AudioClip answer;

    public void NextLevel()
    {
        //Application.LoadLevel("SharkPac");
        SceneManager.I.LoadNextLevel();
    }

    public void PlayQuestion()
    {
        var _blub = GetComponent<AudioSource>();
        _blub.clip = question;
        _blub.Play();
    }

    public void PlayAnswer()
    {
        var _blub = GetComponent<AudioSource>();
        _blub.clip = answer;
        _blub.Play();
    }
}
