using UnityEngine;
using System.Collections;

namespace SharkPac
{
    public class PlayerDude : BasePlayer
    {
        internal override bool GetUserInput()
        {
            //userinput
            hInput = Input.GetAxisRaw("Horizontal");
            vInput = Input.GetAxisRaw("Vertical");

            if (hInput != 0)
                return true;

            if (vInput != 0)
                return true;

            return false;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            //Debug.LogWarning("blub: " + collision, collision.gameObject);
            if (collision.transform.GetComponent<PlayerShark>() != null)
                GameLogic.I.SetState(GameLogic.GameState.SHARKWINS);
        }               
    }
}
