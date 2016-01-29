using UnityEngine;
using System.Collections;
using System;

public class PushAndPull : MonoBehaviour {
    public float pushSpeed = 0.2f;
    public float pushDistance = 10f;

    private bool objectDetected = false;
    private GameObject currentObject;
    // Use this for initialization
    void Start () {
	
	}

    void OnEnable()
    {
        GameObjectDetector.OnDetectedObject+= OnDetectedObject;
        GameObjectDetector.OnExitDetectedObject += OnExitDetectedObject;
    }

    // Update is called once per frame
    void Update () {

        //
        if (Input.GetMouseButtonUp(0))
        {
            if (objectDetected) PushObject(currentObject);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            if (objectDetected)
                TranslateObject(currentObject, currentObject.transform.position - this.transform.forward * pushDistance);
        }
    }


    /// <summary>
    /// Metoda koja se pozove kad se objekt detekta
    /// </summary>
    /// <param name="obj">Objekt koji je detectan</param>
    /// <param name="position">Objektova posljednja pozicija</param>
   void OnDetectedObject(GameObject obj, Vector3 position)
    {
        objectDetected = true;
        currentObject = obj;
        //PushObject(currentObject);

    }
    /// <summary>
    /// Metoda koja se pozove kad se objekt "obj" deselecta
    /// </summary>
    /// <param name="obj">Posljednji objekt</param>
    /// <param name="position">Posljednja pozicija</param>
    void OnExitDetectedObject(GameObject obj, Vector3 position)
    {
        objectDetected = false;
        currentObject = null;
    }


    void PushObject(GameObject obj)
    {
        Debug.Log("PushAndPull - PushObject() ");
        TranslateObject(obj, obj.transform.position + this.transform.forward * pushDistance);
    }
    void TranslateObject(GameObject obj, Vector3 to)
    {
        StartCoroutine(MoveObject(obj, obj.transform.position, to, pushSpeed));

    }

    IEnumerator MoveObject(GameObject obj, Vector3 source, Vector3 target, float overTime)
    {
        Debug.Log("Moving");
        float startTime = Time.time;
        while (Time.time < startTime + overTime)
        {
            obj.transform.position = (Vector3.Lerp(source, target, (Time.time - startTime) / overTime));
            yield return null;
        }
        obj.transform.position = target;
    }
}
