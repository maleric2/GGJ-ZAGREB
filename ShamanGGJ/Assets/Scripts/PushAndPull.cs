using UnityEngine;
using System.Collections;
using System;

public class PushAndPull : MonoBehaviour {


    // Use this for initialization
    void Start () {
	
	}

    void OnEnable()
    {
        GameObjectDetector.OnDetectedObject+= OnDetectedObject;
    }

    // Update is called once per frame
    void Update () {
	}


    /// <summary>
    /// Metoda koja se pozove kad se objekt detekta
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="position"></param>
   public void OnDetectedObject(GameObject obj, Vector3 position)
    {

    }
}
