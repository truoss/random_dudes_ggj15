using UnityEngine;
using SharkInvador;
using System.Collections;



namespace SharkInvador
{
    public class BananaExploder : MonoBehaviour
    {

        public GameObject enemy1;
        public GameObject enemy2;
        public static bool explode = false;
        public static Vector3 position;

        private float time = 0;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            time += Time.deltaTime;

            if (Input.GetAxis("Fire2") > 0 && time > 6)
            {
                time = 0;
                ExplodeAndSpawnEnemy();


            }
        }



        public void ExplodeAndSpawnEnemy()
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");
            int objectCount = objects.Length;

            for (int i = 0; i < objectCount; i++ )
            {
                explode = true;
                position = new Vector3(objects[i].transform.position.x, objects[i].transform.position.y, objects[i].transform.position.z);
                DestroyObject(objects[i]);
                Enemy eenemy1 = (Enemy)Instantiate(enemy1);
                eenemy1.SetPositionAndSpeed();
                explode = true;
                Enemy eenemy2 = (Enemy)Instantiate(enemy2);
                eenemy2.SetPositionAndSpeed();
                explode = false;

            }

            explode = false;

        }
    }
}
