using UnityEngine;
using System.Collections;

public class ObjectMover : MonoBehaviour {

    public Transform[] wayPoints;
    public float speed = 2.0f;

    public int currentWayPoint = 0;

    public bool isSwitchedOn = false;

    void Update()
    {
        if (isSwitchedOn)
        {
            if (transform.position != wayPoints[currentWayPoint].transform.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, wayPoints[currentWayPoint].transform.position, speed * Time.deltaTime);
            }

            if (transform.position == wayPoints[currentWayPoint].transform.position)
            {
                currentWayPoint += 1;
            }

            if (currentWayPoint >= wayPoints.Length)
            {
                currentWayPoint = 0;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.transform.SetParent(gameObject.transform);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }

}
