using UnityEngine;
using System.Collections;

public class SwitchController : MonoBehaviour {

    public ObjectMover movingPlatform = null;

    public DoorMover movingDoor = null;
    public DoorMover movingDoor2 = null;

    private OutlineController outlineController;

	// Use this for initialization
	void Start () {
        outlineController = GetComponent<OutlineController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Player") ||  collider.gameObject.CompareTag("MovableObject")){
            if(movingPlatform != null) movingPlatform.isSwitchedOn = true;
            if (movingDoor != null) movingDoor.isSwitchedOn1 = true;
            if (movingDoor2 != null) movingDoor2.isSwitchedOn2 = true;
            outlineController.AddOutline();
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.CompareTag("Player") ||  collider.gameObject.CompareTag("MovableObject")){
            outlineController.RemoveOutline();
            if (movingPlatform != null) movingPlatform.isSwitchedOn = false;
            if (movingDoor != null) movingDoor.isSwitchedOn1 = false;
            if (movingDoor2 != null) movingDoor2.isSwitchedOn2 = false;
        }
    }
}
