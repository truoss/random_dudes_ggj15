using UnityEngine;
using System.Collections;

public class Stars : MonoBehaviour
{

    public float speed;     //variable for the speed





    // Update is called once per frame
    void Update()
    {
        //set the speed of the background
        float amtToMove = speed * Time.deltaTime;
        transform.Translate(Vector3.down * amtToMove, Space.World);

        //set back to top if background rached the bottom
        if (transform.position.y < -10.75f)
        {
            transform.position = new Vector3(transform.position.x, 14f, transform.position.z);
        }

    }
}
