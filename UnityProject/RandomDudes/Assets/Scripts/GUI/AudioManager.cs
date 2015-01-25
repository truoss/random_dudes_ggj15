using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {


    public AudioClip audio1;
    public AudioClip audio2;
    private int ranStart;

    void Awake()
    {
        //AudioSource audio11 = (AudioSource)audio1;
        ranStart = Random.Range(0, 2);

        if (ranStart == 0)
        {
            audio.clip = audio1;
            audio.Play();
        }
        else
        {
            audio.clip = audio2;
            audio.Play();
        }
    }
}
