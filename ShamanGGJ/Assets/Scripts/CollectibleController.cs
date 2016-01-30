using UnityEngine;
using System.Collections;

public class CollectibleController : MonoBehaviour {

    public GameObject collectedParticle;
    public int value = 1;
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
            if(GameManager.instance != null ) GameManager.instance.AddScore(value);
            collectedParticle.SetActive(true);
            collectedParticle.transform.SetParent(null);
            Destroy(gameObject);
        }
    }
}
