using UnityEngine;
using System.Collections;

namespace SharkPac
{
    public class PlayerShark : BasePlayer
    {
        internal override void DoUpdate()
        {
            int x = 0;
            int y = 0;

            if (Input.GetKey(KeyCode.UpArrow))
            {
                y = 1;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                y = -1;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                x = 1;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                x = -1;
            }

            if (!(x == 0 && y == 0))
                Move(x, y);
        }
    }
}
