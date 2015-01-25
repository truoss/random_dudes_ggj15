using UnityEngine;
/*
#if UNITY_EDITOR
    using UnityEditor;
#endif
 */ 
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;

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
    public Text CountDownText;
    //public Image scorePrefab;
    public Sprite[] leftCharacter;
    public Sprite[] rightCharacter;

    

    public enum CharacterState
    {
        DUDE,
        SHARK,
        BANANA,
        MUNCHPIPE
    }

    void Awake()
    {
        I = this;
        UpdateScoreText();
    }

    public void SetCountDown(int time, UnityAction action)
    {
        StartCoroutine(UpdateCounter(time, action));
        //StartCoroutine(FlashingText(time, action));
    }
    
    IEnumerator UpdateCounter(int time, UnityAction action)
    {
        CountDownText.gameObject.SetActive(true);
        for (int i = time; i == 0; i--)
        {
            CountDownText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        yield return new WaitForSeconds(1);
        CountDownText.gameObject.SetActive(false);

        action();
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
            case CharacterState.MUNCHPIPE:
                LeftPlayerImg.sprite = rightCharacter[3];
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
            case CharacterState.MUNCHPIPE:
                RightPlayerImg.sprite = rightCharacter[3];
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
        /*
#if UNITY_EDITOR
        if (EditorApplication.isPlaying)
            EditorApplication.isPlaying = false;
        else
#endif
         */ 
            Application.Quit();
    }

    void Back()
    {
        ModalPanel.I.modalPanelObject.SetActive(false);
    }
}
