using UnityEngine;
using System.Collections;

public class OutlineController : MonoBehaviour {

    public bool isOutlined = false;

    private MeshRenderer meshRenderer;
    private Color outlineColor;

	// Use this for initialization
	void Start () {
        meshRenderer = GetComponent<MeshRenderer>();
        outlineColor = meshRenderer.material.GetColor("_OutlineColor");
        //Debug.Log(outlineColor);
        meshRenderer.material.SetColor("_OutlineColor", new Color(1.0f, 0.0f, 0.0f, 0.0f));
	}
	
	// Update is called once per frame
	void Update () {
        /*if (isOutlined)
        {
            AddOutline();
        }
        else
        {
            RemoveOutline();
        }*/
	}


    public void AddOutline()
    {
        meshRenderer.material.SetColor("_OutlineColor", outlineColor);
    }

    public void RemoveOutline()
    {
        meshRenderer.material.SetColor("_OutlineColor", new Color(1.0f, 0.0f, 0.0f, 0.0f));
    }
}
