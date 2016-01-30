using UnityEngine;
using System.Collections;

public class DoorMover : MonoBehaviour {

    public Transform startWayPoint;
    public Transform EndWayPoint;
    public float speed = 2.0f;

    public bool isSwitchedOn1 = false;
    public bool isSwitchedOn2 = false;

    void Update()
    {
        if (isSwitchedOn1 && isSwitchedOn2)
        {
            if (transform.position != EndWayPoint.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, EndWayPoint.position, speed * Time.deltaTime);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startWayPoint.position, speed * Time.deltaTime);          
        }
    }


}
