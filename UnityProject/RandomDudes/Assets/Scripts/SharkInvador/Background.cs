using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

    public Texture Background1;
    public Texture Background2;
    public Texture Background3;
    private int selection = 0;


	// Use this for initialization
	void Start () 
    {
        selection = Random.Range(0, 3);
        switch (selection)
        {
            case (0):
                renderer.material.mainTexture = Background1;
                break;

            case (1):
                renderer.material.mainTexture = Background2;
                break;

            case (2):
                renderer.material.mainTexture = Background3;
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
