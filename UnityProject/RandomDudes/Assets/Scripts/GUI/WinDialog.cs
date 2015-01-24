using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinDialog : MonoBehaviour 
{
    public static WinDialog I;
    public Image LeftWin;
    public Image RightWin;

    public ImageState curState; 
    public enum ImageState
    {
        LEFT,
        RIGHT,
        NONE
    }

    void Awake()
    {
        I = this;
        SetImageState(ImageState.NONE);
    }

    public void SetImageState(ImageState state)
    {
        curState = state;

        switch (curState)
        {
            case ImageState.LEFT:
                LeftWin.gameObject.SetActive(true);
                RightWin.gameObject.SetActive(false);
                break;
            case ImageState.RIGHT:
                LeftWin.gameObject.SetActive(false);
                RightWin.gameObject.SetActive(true);
                break;
            case ImageState.NONE:
                LeftWin.gameObject.SetActive(false);
                RightWin.gameObject.SetActive(false);
                break;
            default:
                break;
        }

    }
}
