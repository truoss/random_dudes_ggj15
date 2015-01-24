using UnityEngine;
using System.Collections;

namespace SharkPac
{
    public class Target : MonoBehaviour
    {
        void OnCollisionEnter2D(Collision2D collision)
        {
            var _tmp = collision.transform.GetComponent<Player>();
            //
            if (_tmp != null)
            {
                //who is colliding
                //Debug.LogWarning("isPlayer1: " + _tmp.isPlayer1, _tmp);
                //Debug.LogWarning("character: " + _tmp.character, _tmp);


                if (_tmp.character == MainUI.CharacterState.DUDE)
                {
                    Debug.LogWarning("Target: " + collision.transform.GetComponent<Player>().character, collision.gameObject);

                    if (_tmp.isPlayer1)
                        GameLogic.I.SetState(GameLogic.GameState.PLAYER1WINS);
                    else
                        GameLogic.I.SetState(GameLogic.GameState.PLAYER2WINS);                    
                }  
            }  
        }
    }
}
