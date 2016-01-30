using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(GameObjectDetector))]
public class PushAndPull : MonoBehaviour
{

    public float pushSpeed =10f;
    public float pushDistance = 6f;
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
                MoveObjectPush(currentObject, this.transform.forward, false);
            }
            else if (Input.GetMouseButtonDown(1) && objectDetected)
            {
                MoveObjectPush(currentObject, -this.transform.forward, true);
            }
        }
        else
        {
            if (Input.GetMouseButton(0) && objectDetected)
            {
                MoveObject(currentObject, this.transform.forward, pushSpeed, false);
            }
            else if (Input.GetMouseButton(1) && objectDetected)
            {
                MoveObject(currentObject, -this.transform.forward, pushSpeed, true);
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
        if (obj.GetComponent<Rigidbody>() != null)
        {
            objectDetected = true;
            currentObject = obj;
            if (obj.GetComponent<OutlineController>())
                obj.GetComponent<OutlineController>().AddOutline();
        }
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

    void MoveObjectPush(GameObject obj, Vector3 to, bool pull)
    {
        Rigidbody objRb = obj.GetComponent<Rigidbody>();

        Vector3 wantedPosition = to * pushSpeed * pushDistance;
        float currDistance = Vector3.Distance(this.transform.position, obj.transform.position);
        float wantedDistance = Vector3.Distance(this.transform.position, wantedPosition);

        //Debug.Log("MoveObj: distance: " + currDistance + " wantedDist: " + wantedDistance);
        if (currDistance > distanceFromObject || !pull)
            objRb.AddForce(to * pushSpeed * pushDistance * objRb.mass, ForceMode.Impulse);

    }

 
    void MoveObject(GameObject obj, Vector3 target, float speed, bool pull)
    {
        Rigidbody objRb = obj.GetComponent<Rigidbody>();

        Vector3 wantedPosition = target * pushSpeed * objRb.mass;
        float currDistance = Vector3.Distance(this.transform.position, obj.transform.position);
        float wantedDistance = Vector3.Distance(this.transform.position, wantedPosition);


        //Debug.Log("MoveObj: distance: " + currDistance + " wantedDist: " + wantedDistance);
        if (currDistance > distanceFromObject || !pull)
            objRb.AddForce(target * pushSpeed * objRb.mass, ForceMode.Force);
    }
}
