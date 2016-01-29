using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GameObjectDetector : DirectionDetector<GameObject>
{

    public override GameObject GetObject(GameObject hitted)
    {
        Debug.Log("Detected " + hitted.gameObject.name);
        return hitted;
    }

    void Update()
    {

        this.DoLogic();
    }

}
