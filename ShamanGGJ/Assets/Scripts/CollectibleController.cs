using UnityEngine;
using System.Collections;

public class CollectibleController : MonoBehaviour {

    public GameObject collectedParticle;

    private bool isCollected = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter(Collider collider){

        if(!isCollected && collider.gameObject.CompareTag("Player")){
            isCollected = true;
            GameManager.instance.UpdateScore();
            collectedParticle.SetActive(true);
            collectedParticle.transform.SetParent(null);
            Destroy(gameObject);
        }
    }
}
