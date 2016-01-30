using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {

    public Transform checkPoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collider)
    {
            Debug.Log("WATAFAK");
            collider.gameObject.transform.position = checkPoint.position;
    }
}
