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
        }

        void Start()
        {
            SetState(GameState.PLAYING);
        }

        public void SetState(GameState state)
        {
            curState = state;
            
        }

        public IEnumerator Wait(int p, GameLogic.GameState state)
        {
            yield return new WaitForSeconds(p);

            GameLogic.I.SetState(state);
        }
    }
}
