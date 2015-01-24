using UnityEngine;
using System.Collections;

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
                        MainUI.I.SetLeftCharacter(MainUI.CharacterState.DUDE);
                        MainUI.I.SetRightCharacter(MainUI.CharacterState.DUDE);
                    }
                    Spawner.I.Init();
                    break;
                case GameState.PLAYING:
                    break;
                case GameState.LEFTWINS:
                    break;
                case GameState.RIGHTWINS:
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
    }
}
