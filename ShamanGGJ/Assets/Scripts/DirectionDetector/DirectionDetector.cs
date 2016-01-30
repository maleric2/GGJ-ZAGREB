using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class DirectionDetector<T> : MonoBehaviour
{
    public delegate void DirectionDetectorEvent(T obj, Vector3 pos);
    public static event DirectionDetectorEvent OnDetectedObject;
    public static event DirectionDetectorEvent OnExitDetectedObject;

    public Transform directionDetector;

    //public string tag = "";
    public float rayMaxDistance = 100f;

    public LayerMask layer;

    private T lastObject;
    private Vector3 lastPosition;

    [Tooltip("Disable by default if you dont know how it will work")]
    public bool enableContiniousEvents = false;
    private bool doNotContiniousDetect = false;
    void Update()
    {
        DoLogic();
    }
    public void DoLogic()
    {
        DoLogic3D();
    }

    /// <summary>
    /// Works only for 2D objects with 2D colliders
    /// </summary>
    /// <param name="position"></param>
    void DoLogic2D()
    {
        /*RaycastHit2D hit;
        Ray ray = Camera.main.ScreenPointToRay(position);
        hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit != null && hit.collider != null)
        {
            RegisterEvents(hit.collider.gameObject, hit.point);
        }*/
    }
    /// <summary>
    /// Works only for 3D objects with 3D colliders
    /// </summary>
    /// <param name="position"></param>
    void DoLogic3D()
    {
        //Debug.Log("Doing Logic");
        RaycastHit hit;

        Vector3 position;

        if (directionDetector != null)
            position = directionDetector.position;
        else
            position = this.transform.position + Vector3.up * 0.4f;

        Vector3 fwd = transform.TransformDirection(Vector3.forward) * rayMaxDistance;

        if (enableContiniousEvents) doNotContiniousDetect = false;

        if (Physics.Raycast(position, fwd, out hit, rayMaxDistance, layer))
        {
            Debug.DrawRay(position, fwd, Color.cyan, 5, false);

            if (hit.collider != null && !doNotContiniousDetect)
            {
                doNotContiniousDetect = true;
                RegisterEvents(hit.collider.gameObject, hit.point);
            }
        }
        else
        {
            if (doNotContiniousDetect && lastPosition!=null && lastObject !=null)
                if (OnExitDetectedObject != null)
                {
                    OnExitDetectedObject(lastObject, lastPosition);
                }
            doNotContiniousDetect = false;
        }
    }

    /// <summary>
    /// Method for registering Events. 
    /// Can be overrided if there are multiple events and multiple tags
    /// </summary>
    /// <param name="hitted"></param>
    /// <param name="position"></param>
    public void RegisterEvents(GameObject hitted, Vector3 position)
    {
        if (OnDetectedObject != null)
        {
            OnDetectedObject(GetObject(hitted), position);
            lastObject = GetObject(hitted);
            lastPosition = position;
        }
    }

    /// <summary>
    /// Object To Save, GameObject or something else
    /// </summary>
    /// <param name="hitted"></param>
    /// <returns></returns>
    public abstract T GetObject(GameObject hitted);
}
