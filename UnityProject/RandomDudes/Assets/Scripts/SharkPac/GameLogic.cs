using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace SharkPac
{
    public class GameLogic : MonoBehaviour
    {
        public static GameLogic I;
        public GameState curState;
        public enum GameState
        {
            START,
            PLAYING,
            PLAYER2WINS,
            PLAYER1WINS
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
                        if (Random.Range(0, 2) > 0.5)
                        {
                            MainUI.I.SetLeftCharacter(MainUI.CharacterState.DUDE);
                            MainUI.I.SetRightCharacter(MainUI.CharacterState.SHARK);
                        }
                        else
                        {
                            MainUI.I.SetLeftCharacter(MainUI.CharacterState.SHARK);
                            MainUI.I.SetRightCharacter(MainUI.CharacterState.DUDE);
                        }
                    }
                    FieldManager.I.InitFields();                    
                    break;
                case GameState.PLAYING:
                    break;
                case GameState.PLAYER2WINS:
                    MainUI.I.AddRightPlayerScore();
                    WinDialog.I.SetImageState(WinDialog.ImageState.RIGHT);
                    //SetState(GameState.START);
                    StartCoroutine(Wait(3, (UnityAction)SceneManager.I.LoadVulcano));
                    break;
                case GameState.PLAYER1WINS:
                    MainUI.I.AddLeftPlayerScore();
                    WinDialog.I.SetImageState(WinDialog.ImageState.LEFT);
                    //SetState(GameState.START);
                    StartCoroutine(Wait(3, (UnityAction)SceneManager.I.LoadVulcano));
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
