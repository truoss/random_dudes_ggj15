using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SharkVulcano
{
    public class Spawner : MonoBehaviour
    {
        public static Spawner I;
        public Transform[] spawnPoints;
        //public List<Transform> ballPool = new List<Transform>();
        public Transform ballPrefab;

        void Awake()
        {
            I = this;
        }

        public void Init()
        {
            //Invoke("SpawnBall", 1);
            InvokeRepeating("SpawnBall", 1, 0.5f);
            GameLogic.I.SetState(GameLogic.GameState.PLAYING);
        }

        /*
        void Update()
        {
            if (GameLogic.I.curState == GameLogic.GameState.RIGHTWINS || GameLogic.I.curState == GameLogic.GameState.LEFTWINS)
                CancelInvoke();
        }
        */

        void SpawnBall()
        {
            //Debug.LogWarning("Blub", this);
            var obj = Instantiate(ballPrefab, spawnPoints[Random.Range(0, (spawnPoints.Length))].position, Quaternion.identity) as Transform;
            obj.parent = transform;
            var rig = obj.GetComponent<Rigidbody>();
            if (rig)
            {
                rig.angularVelocity = new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), 0);
                //rig.AddTorque(new Vector3(Random.Range(50, 180), Random.Range(50, 180), 0));
                rig.AddForce(new Vector3(Random.Range(-500, 500), -8000, 0));
            }
            //ballPool.Add(obj);
        }        
    }
}
