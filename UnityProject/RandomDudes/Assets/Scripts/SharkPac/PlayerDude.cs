using UnityEngine;
using System.Collections;

namespace SharkPac
{
    public class PlayerDude : BasePlayer
    {        
        internal override void DoUpdate()
        {
            int x = 0;
            int y = 0;

            if (Input.GetKey(KeyCode.W))
            {
                y = 1;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                y = -1;
            }

            if (Input.GetKey(KeyCode.D))
            {
                x = 1;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                x = -1;
            }

            if(!(x == 0 && y == 0))
                Move(x, y);
        }
    }
}
