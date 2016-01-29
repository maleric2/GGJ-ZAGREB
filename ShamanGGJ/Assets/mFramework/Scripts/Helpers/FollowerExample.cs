using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class FollowerExample : MonoBehaviour {

    public float speed = 20f;
    bool followPlayer = false;

    //Mora bit isto kao u SightDetectoru
    public GameObject target;

	// Use this for initialization
	void Start () {

        //Provjerit postoji li komponenta i dohvatit od tamo target
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void FollowPlayer(bool follow)
    {
        followPlayer = follow;
    }
    public void Move()
    {
        if (followPlayer)
        {
            transform.LookAt(target.transform);
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance > 2.2f)
            {
                transform.position += transform.forward * speed * Time.deltaTime;
                if(this.GetComponent<Animator>()!=null)
                    this.GetComponent<Animator>().SetFloat("Speed", speed);
            }
        }
        else
        {
            if(this.GetComponent<Animator>()!=null)
                this.GetComponent<Animator>().SetFloat("Speed", 0);
        }
    }
}
