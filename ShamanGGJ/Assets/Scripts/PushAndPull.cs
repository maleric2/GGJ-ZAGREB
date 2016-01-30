using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(GameObjectDetector))]
public class PushAndPull : MonoBehaviour
{

    public float pushSpeed =6f;
    public float pushDistance = 20f;
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
                MoveObjectPush(currentObject, this.transform.forward);
            }
            else if (Input.GetMouseButtonDown(1) && objectDetected)
            {
                MoveObjectPush(currentObject, -this.transform.forward);
            }
        }
        else
        {
            if (Input.GetMouseButton(0) && objectDetected)
            {
                MoveObject(currentObject, this.transform.forward, pushSpeed);
            }
            else if (Input.GetMouseButton(1) && objectDetected)
            {
                MoveObject(currentObject, -this.transform.forward, pushSpeed);
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

    void MoveObjectPush(GameObject obj, Vector3 to)
    {
        Debug.Log("PushAndPull - PushObject() ");
        Rigidbody objRb = obj.GetComponent<Rigidbody>();

        Vector3 wantedPosition = to * pushDistance * pushSpeed * objRb.mass;
        float currDistance = Vector3.Distance(this.transform.position, obj.transform.position);
        float wantedDistance = Vector3.Distance(this.transform.position, wantedPosition);

        if (wantedDistance > currDistance || currDistance > distanceFromObject)
            objRb.AddForce(to * pushDistance * pushSpeed * objRb.mass, ForceMode.Acceleration);

    }

 
    void MoveObject(GameObject obj, Vector3 target, float speed)
    {
        Rigidbody objRb = obj.GetComponent<Rigidbody>();

        Vector3 wantedPosition = target * pushDistance * pushSpeed * objRb.mass;
        float currDistance = Vector3.Distance(this.transform.position, obj.transform.position);
        float wantedDistance = Vector3.Distance(this.transform.position, wantedPosition);

        if (wantedDistance > currDistance || currDistance > distanceFromObject)
            objRb.AddForce(target * pushSpeed * objRb.mass, ForceMode.Acceleration);
    }
}
