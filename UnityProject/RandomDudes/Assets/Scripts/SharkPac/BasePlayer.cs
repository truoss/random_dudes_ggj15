using UnityEngine;
using System.Collections;

namespace SharkPac
{
    public class BasePlayer : MonoBehaviour
    {
        public int xCoord;
        public int yCoord;
        float updateTime = 0.25f;

        void Start()
        {
            InvokeRepeating("DoUpdate", 0, updateTime);
        }

        void Update()
        {
            if (transform.position != new Vector3(xCoord, yCoord, 0))
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(xCoord, yCoord, 0), Time.deltaTime*2);
            }
        }

        public void Move(int x, int y)
        {
            if (FieldManager.I.CheckIfMovePossible(this, x, y))
            {
                FieldManager.I.SetPlayerPosition(this, xCoord + x, yCoord + y);
            }
        }        

        internal virtual void DoUpdate(){}

    }
}

