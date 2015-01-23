using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{

    public float projectileSpeed;       // defines the speed of the projectile
    public GameObject ExplosionPrefab;  // to add objekt explosion
    private Enemy enemy;                //gets enemy






    //set enemy
    void Start()
    {
        enemy = (Enemy)GameObject.Find("Enemy").GetComponent("Enemy");
    }






    // Update is called once per frame
    void Update()
    {

        //move projectile
        float amtToMove = projectileSpeed * Time.deltaTime;
        transform.Translate(Vector3.up * amtToMove);

    }







    //Destroy the projectile that leaves the screen
    void OnBecameInvisible()
    {

        Destroy(gameObject);
    }








    //check for collissions
    void OnTriggerEnter(Collider otherObject)
    {
        //for debug purposes
        Debug.Log("We hit: " + otherObject.name);

        //when enemy hit
        if (otherObject.tag == "Enemy")
        {
            //increase score
            Player.score += 100;
            Player.scoreAmmo += 100;

            if (Player.score >= 100000)
                Application.LoadLevel(3);

            //reset the enemy
            Instantiate(ExplosionPrefab, enemy.transform.position, enemy.transform.rotation);
            enemy.minSpeed += 0.01f;
            enemy.maxSpeed += 0.1f;
            enemy.SetPositionAndSpeed();

            //////////////////////////
            //////////////////////////
            /////    TO   DO     /////
            //////////////////////////
            //////////////////////////
            //different explosions

            //Destroy projectile
            Destroy(gameObject);
        }
    }
}
