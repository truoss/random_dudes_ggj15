using UnityEngine;
#if UNITY_EDITOR
    using UnityEditor;
#endif
using UnityEngine.UI;
using System.Collections;

public class MainUI : MonoBehaviour 
{
    public static MainUI I;
    public RectTransform LeftPlayer;
    public Image LeftPlayerImg;
    public RectTransform RightPlayer;
    public Image RightPlayerImg;
    public CharacterState LeftPlayerCharacter;
    public CharacterState RightPlayerCharacter;
    public static int LeftPlayerScore = 0;
    public static int RightPlayerScore = 0;
    public Text LeftScoreText;
    public Text RightScoreText;
    //public Image scorePrefab;
    public Sprite[] leftCharacter;
    public Sprite[] rightCharacter;
    public enum CharacterState
    {
        DUDE,
        SHARK,
        BANANA
    }

    void Awake()
    {
        I = this;
        UpdateScoreText();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!ModalPanel.I.modalWindowOpen)
                ShowCloseWindow();
        }
    }

    public void SetLeftCharacter(CharacterState charState)
    {
        LeftPlayerCharacter = charState;

        switch (LeftPlayerCharacter)
        {
            case CharacterState.DUDE:
                LeftPlayerImg.sprite = leftCharacter[0];
                break;
            case CharacterState.SHARK:
                LeftPlayerImg.sprite = leftCharacter[1];
                break;
            case CharacterState.BANANA:
                LeftPlayerImg.sprite = leftCharacter[2];
                break;
            default:
                break;
        }
    }

    public void SetRightCharacter(CharacterState charState)
    {
        RightPlayerCharacter = charState;

        switch (RightPlayerCharacter)
        {
            case CharacterState.DUDE:
                RightPlayerImg.sprite = rightCharacter[0];
                break;
            case CharacterState.SHARK:
                RightPlayerImg.sprite = rightCharacter[1];
                break;
            case CharacterState.BANANA:
                RightPlayerImg.sprite = rightCharacter[2];
                break;
            default:
                break;
        }
    }

    public void AddLeftPlayerScore()
    {
        LeftPlayerScore++;
        //var obj = Instantiate
        //Debug.LogWarning("Left: " + LeftPlayerScore + " : Right: " + RightPlayerScore);
        UpdateScoreText();
    }

    public void AddRightPlayerScore()
    {
        RightPlayerScore++;
        //Debug.LogWarning("Left: " + LeftPlayerScore + " : Right: " + RightPlayerScore);
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        LeftScoreText.text = LeftPlayerScore.ToString();
        RightScoreText.text = RightPlayerScore.ToString();
    }

    public void ShowCloseWindow()
    {
        ModalPanel.I.Choice("Would you like to Quit?", Quit, Back);
    }

    void Quit()
    {
#if UNITY_EDITOR
        if (EditorApplication.isPlaying)
            EditorApplication.isPlaying = false;
        else
#endif
            Application.Quit();
    }

    void Back()
    {
        ModalPanel.I.modalPanelObject.SetActive(false);
    }
}
