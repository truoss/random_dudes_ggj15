using UnityEngine;
using System.Collections;

namespace SharkPac
{
    public class Field : MonoBehaviour
    {
        public FieldState currentState;

        public enum FieldState
        {
            FREE,
            BLOCKED,
            PLAYER1START,
            PLAYER2START,
            TARGET
        }

        /*
        public Player isPlayerOn = null;

        public void Init()
        {
            switch (currentState)
            {
                case FieldState.FREE:
                 break;
                case FieldState.BLOCKED:
                 break;
                case FieldState.PLAYER1START:
                
                 break;
                case FieldState.PLAYER2START:
                 break;
                case FieldState.TARGET:
                 break;
                default:
                 break;
            }
        
        }
        */
    }
}
