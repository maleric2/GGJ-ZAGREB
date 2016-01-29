using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class SightDetector : MonoBehaviour
{
    public GameObject target;

    public delegate void SightDetectorEvent(GameObject obj, string tag, Vector3 pos);
    public static event SightDetectorEvent OnPlayerSight;
    public static event SightDetectorEvent OnOtherSight;

    public float fieldOfViewAngle = 120f;
    public bool playerInSight;                      // Whether or not the player is currently sighted.
    public Vector3 personalLastSighting = Vector3.zero;

    private Vector3 previousSighting = Vector3.zero;

    // Sight to avoid objects on close range
    public GameObject miniSight;

    private bool doNotContiniousDetect = false;
    
    /// <summary>
    /// Provjeravanje eventa po tagovima (nije najmodularnije)
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerStay(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        if (other.gameObject == target || other.tag == "Enemy")
        {
            playerInSight = false;

            // Create a vector from the enemy to the player and store the angle between it and forward.
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            // If the angle between forward and where the player is, is less than half the angle of view...
            if (angle < fieldOfViewAngle * 0.5f)
            {
                RaycastHit hit;
                Debug.DrawRay(transform.position, direction, Color.green, 1, false);
                if (Physics.Raycast(transform.position, direction, out hit, this.gameObject.GetComponent<SphereCollider>().radius))
                {
                    // ... and if the raycast hits the player...
                    if (hit.collider.gameObject == target)
                    {
                        // ... the player is in sight.
                        playerInSight = true;

                        // Set the last global sighting is the players current position.
                        personalLastSighting = target.transform.position;

                        //Debug.Log("I can see you");

                        if (!doNotContiniousDetect)
                            OnPlayerSight(this.gameObject, hit.collider.tag, target.transform.position);
                    }

                    else if (hit.collider.tag.Equals("Enemy"))
                    {
                        OnOtherSight(this.gameObject, hit.collider.tag, Vector3.zero);
                    }
                    else
                        doNotContiniousDetect = true;
                }
            }


        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == target || other.tag == "Enemy")
        {
            doNotContiniousDetect = false;
        }
    }
}
