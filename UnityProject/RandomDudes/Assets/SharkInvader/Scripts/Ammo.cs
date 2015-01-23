using UnityEngine;
using System.Collections;

public class Ammo : MonoBehaviour
{

    #region Fields
    public float currentSpeed;              //the current speed of ammo
    private float x, y, z;                  //coordinates of random new position
    private float currentRotationSpeed;     //current rotation speed
    #endregion





    // Update is called once per frame
    void Update()
    {
        if (Player.scoreAmmo >= 1000)
        {
            SetPositionAndSpeed();
            Player.scoreAmmo = 0;
            Player.receivedAmmo = true;
        }


        //set the rotation speed of enemy
        float rotationSpeed = currentRotationSpeed * Time.deltaTime;
        transform.Rotate(new Vector3(0, 1, 0) * rotationSpeed);

        //move enemy
        float amtToMove = currentSpeed * Time.deltaTime;
        transform.Translate(Vector3.down * amtToMove, Space.World);

        // ScreenWrap
        // lets the enemy reapear at the other side if the screeen horizontal
        if (transform.position.x < -6.6f)
            transform.position = new Vector3(6.6f, transform.position.y, transform.position.z);
        else if (transform.position.x > 6.6f)
            transform.position = new Vector3(-6.6f, transform.position.y, transform.position.z);

    }







    //function to inizialize the positon and speed
    public void SetPositionAndSpeed()
    {
        //get randoms for rotation
        currentRotationSpeed = 120f;

        //set new speed
        currentSpeed = 2f;

        //set new position
        x = Random.Range(-6f, 6f);
        y = 7f;
        z = 0f;
        transform.position = new Vector3(x, y, z);


    }




    //function to inizialize the positon and speed after hit
    public void SetPositionAndSpeedAfter()
    {
        //get randoms for rotation
        currentRotationSpeed = 120f;

        //set new speed
        currentSpeed = 2f;

        //set new position
        x = Random.Range(-6f, 6f);
        y = -15f;
        z = 0f;
        transform.position = new Vector3(x, y, z);


    }








    //set new position and speed when enemy has reached the bottom of the screen
    void OnBecameInvisible()
    {
        //Destroy(gameObject);
    }
}
