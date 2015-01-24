using UnityEngine;
using System.Collections;

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
            SHARKWINS,
            DUDEWINS
        }        

        void Awake()
        {
            I = this;            
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
                    FieldManager.I.InitFields();                    
                    break;
                case GameState.PLAYING:
                    break;
                case GameState.SHARKWINS:
                    SetState(GameState.START);
                    break;
                case GameState.DUDEWINS:
                    SetState(GameState.START);
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
