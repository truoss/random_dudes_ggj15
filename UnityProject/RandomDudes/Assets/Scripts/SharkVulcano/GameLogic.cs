using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

namespace SharkVulcano
{
    public class GameLogic : MonoBehaviour
    {
        public static GameLogic I;
        public GameState curState;
        public enum GameState
        {
            START,
            PLAYING,
            LEFTWINS,
            RIGHTWINS
        }

        void Awake()
        {
            I = this;

            Application.LoadLevelAdditive(0);
        }

        void Start()
        {
            SetState(GameState.START);
        }

        public void SetState(GameState state)
        {
            curState = state;

            switch (curState)
            {
                case GameState.START:
                    if (MainUI.I)
                    {
                        //GUI
                        MainUI.I.SetLeftCharacter(MainUI.CharacterState.DUDE);
                        MainUI.I.SetRightCharacter(MainUI.CharacterState.DUDE);
                    }
                    Spawner.I.Init();
                    break;
                case GameState.PLAYING:
                    break;
                case GameState.LEFTWINS:
                    MainUI.I.AddLeftPlayerScore();
                    WinDialog.I.SetImageState(WinDialog.ImageState.LEFT);
                    //SetState(GameState.START);
                    StartCoroutine(Wait(3, (UnityAction)SceneManager.I.LoadPac));
                    break;
                case GameState.RIGHTWINS:
                    MainUI.I.AddRightPlayerScore();
                    WinDialog.I.SetImageState(WinDialog.ImageState.RIGHT);
                    //SetState(GameState.START);
                    StartCoroutine(Wait(3, (UnityAction)SceneManager.I.LoadPac));
                    break;
                default:
                    break;
            }
            
        }

        public IEnumerator Wait(int p, GameLogic.GameState state)
        {
            yield return new WaitForSeconds(p);

            GameLogic.I.SetState(state);
        }

        public IEnumerator Wait(int p, UnityAction action)
        {
            yield return new WaitForSeconds(p);

            action();
        }
    }
}
