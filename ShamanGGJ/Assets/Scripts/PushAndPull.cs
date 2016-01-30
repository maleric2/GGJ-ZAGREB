using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(GameObjectDetector))]
public class PushAndPull : MonoBehaviour
{

    public float fastPushInterval = 0.3f;
    public float pushSpeed = 0.02f;
    public float pushDistance = 10f;
    public float distanceFromObject = 2f;

    private bool objectDetected = false;
    private GameObject currentObject;

    private bool shiftClickMove = false;
    // Use this for initialization
    void Start()
    {

    }

    void OnEnable()
    {
        GameObjectDetector.OnDetectedObject += OnDetectedObject;
        GameObjectDetector.OnExitDetectedObject += OnExitDetectedObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetMouseButtonDown(0) && objectDetected)
            {
                TranslateObject(currentObject, currentObject.transform.position + this.transform.forward * pushDistance);
            }
            else if (Input.GetMouseButtonDown(1) && objectDetected)
            {
                TranslateObject(currentObject, currentObject.transform.position - this.transform.forward * pushDistance);
            }
        }
        else
        {
            if (Input.GetMouseButton(0) && objectDetected)
            {
                MoveObject(currentObject, currentObject.transform.position + this.transform.forward * pushDistance, pushSpeed);
            }
            else if (Input.GetMouseButton(1) && objectDetected)
            {
                MoveObject(currentObject, currentObject.transform.position - this.transform.forward * pushDistance, pushSpeed);
            }
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
        if (obj.GetComponent<OutlineController>())
            obj.GetComponent<OutlineController>().AddOutline();
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
        if (obj.GetComponent<OutlineController>())
            obj.GetComponent<OutlineController>().RemoveOutline();
    }

    void TranslateObject(GameObject obj, Vector3 to)
    {
        Debug.Log("PushAndPull - PushObject() ");
        //Da se ne poziva vise puta (izgleda kao da trza jer prethodna animacije nije gotova) provjeravamo mo dali je izvršen posljednji pokret
        if(!shiftClickMove)
            StartCoroutine(MoveObject(obj, obj.transform.position, to, fastPushInterval));

    }


    IEnumerator MoveObject(GameObject obj, Vector3 source, Vector3 target, float overTime)
    {
        Debug.Log("Moving");
        shiftClickMove = true;
        float startTime = Time.time;


        while (Time.time < startTime + overTime)
        {
            Vector3 wantedPosition = Vector3.Lerp(source, target, (Time.time - startTime) / overTime);
            float currDistance = Vector3.Distance(this.transform.position, obj.transform.position);
            float wantedDistance = Vector3.Distance(this.transform.position, wantedPosition);

            if (currDistance < distanceFromObject && wantedDistance < currDistance)
            {
                shiftClickMove = false;
                yield break;
            }
            else
            {
                obj.transform.position = wantedPosition;
                yield return null;
            }

        }
        obj.transform.position = target;
        shiftClickMove = false;
    }

    void MoveObject(GameObject obj, Vector3 target, float speed)
    {
        Vector3 wantedPosition = Vector3.Lerp(obj.transform.position, target, speed);
        float currDistance = Vector3.Distance(this.transform.position, obj.transform.position);
        float wantedDistance = Vector3.Distance(this.transform.position, wantedPosition);

        if (wantedDistance > currDistance || currDistance > distanceFromObject)
            obj.transform.position = wantedPosition;
    }
}
