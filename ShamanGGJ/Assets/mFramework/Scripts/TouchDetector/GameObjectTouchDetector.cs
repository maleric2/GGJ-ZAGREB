using UnityEngine;
using System.Collections;
/// <summary>
/// Class for Detecting GameObject
/// EventListener trigger event with (GameObject, position)
/// Usage for events: GameObjectTouchDetector.OnTouch += OnTouchObject;
/// </summary>
public class GameObjectTouchDetector : TouchDetector<GameObject> {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override GameObject GetObject(GameObject hitted)
    {
        return hitted;
    }
}
