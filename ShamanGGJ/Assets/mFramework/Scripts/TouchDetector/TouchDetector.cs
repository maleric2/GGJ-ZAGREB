using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class TouchDetector<T> : MonoBehaviour
{
    public delegate void TouchDetectorEvent(T obj, Vector3 pos);
    public static event TouchDetectorEvent OnTouch;

    public string tag = "";
    public bool mode3D = false;

    // Update is called once per frame
    void Update()
    {
        doPc();
    }
    void doMobile()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                DoLogic(Input.mousePosition);
            }
        }
    }
    void doPc()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DoLogic(Input.mousePosition);
        }
    }

    void DoLogic(Vector3 position)
    {
        if (!mode3D) DoLogic2D(position);
        else DoLogic3D(position);
    }

    /// <summary>
    /// Works only for 2D objects with 2D colliders
    /// </summary>
    /// <param name="position"></param>
    void DoLogic2D(Vector3 position)
    {
        RaycastHit2D hit;
        Ray ray = Camera.main.ScreenPointToRay(position);
        hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit != null && hit.collider != null)
        {
            RegisterEvents(hit.collider.gameObject, hit.point);
        }
    }
    /// <summary>
    /// Works only for 3D objects with 3D colliders
    /// </summary>
    /// <param name="position"></param>
    void DoLogic3D(Vector3 position)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(position);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                RegisterEvents(hit.collider.gameObject, hit.point);
            }
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
        if (hitted.tag.Equals(tag))
        {
            if (OnTouch != null) OnTouch(GetObject(hitted), position);
        }
    }
    /// <summary>
    /// Object To Save, GameObject or something else
    /// </summary>
    /// <param name="hitted"></param>
    /// <returns></returns>
    public abstract T GetObject(GameObject hitted);
}
