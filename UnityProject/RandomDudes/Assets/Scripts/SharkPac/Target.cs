using UnityEngine;
using System.Collections;

namespace SharkPac
{
    public class Target : MonoBehaviour
    {
        void OnCollisionEnter2D(Collision2D collision)
        {
            //Debug.LogWarning("Target: " + collision, collision.gameObject);
            if (collision.transform.GetComponent<Player>() != null)
            {
                if (collision.transform.GetComponent<Player>().character == MainUI.CharacterState.DUDE)
                    GameLogic.I.SetState(GameLogic.GameState.DUDEWINS);
            }  
        }
    }
}
