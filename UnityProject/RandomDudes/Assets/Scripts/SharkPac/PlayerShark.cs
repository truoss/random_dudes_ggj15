using UnityEngine;
using System.Collections;

namespace SharkPac
{
    public class PlayerShark : BasePlayer
    {
        internal override bool GetUserInput()
        {
            //userinput
            hInput = Input.GetAxisRaw("Horizontal2");
            vInput = Input.GetAxisRaw("Vertical2");

            if (hInput != 0)
                return true;

            if (vInput != 0)
                return true;

            return false;
        }
    }
}
