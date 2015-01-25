using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour {

    public void NextLevel()
    {
        //Application.LoadLevel("SharkPac");
        SceneManager.I.LoadNextLevel();
    }

    public void PlayAudio()
    {
        var _blub = GetComponent<AudioSource>();
        _blub.Play();
    }
}
