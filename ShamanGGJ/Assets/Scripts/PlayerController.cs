using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class PlayerController : MonoBehaviour {

    public float speed = 5.0f;
    public float m_JumpPower = 6.0f;

    public float camRayLength = 100.0f;
    public LayerMask floorMask; 

    Vector3 movement;
    Animator animator;
    Rigidbody playerRigidbody;

    private LocomotionsPlayer movController;

    public Transform groundCheck;

    [SerializeField]
    float groundCheckDistance = 0.1f;

    [SerializeField]
    bool isGrounded = true;

    void Awake()
    {
        //Cursor.visible = false;

        animator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        movController = new LocomotionsPlayer(this.gameObject, speed);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        movController.GetInputs();
	}

    void FixedUpdate()
    {
        Move();
        //movController.OnMouseMove();
        Animating();
        Turning();
        CheckGroundStatus();
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    void Move()
    {
        movController.Move();
    }

    void Turning()
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            playerRigidbody.MoveRotation(newRotation);
        }

    }

    void Animating()
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        //bool running = moveH != 0f || moveV != 0f;

        // Tell the animator whether or not the player is walking.
        //animator.SetBool("isRunning", running);
    }

    void CheckGroundStatus()
    {
        RaycastHit hitInfo;
#if UNITY_EDITOR
        // helper to visualise the ground check ray in the scene view
        Debug.DrawLine(groundCheck.position + (Vector3.up * 0.1f), groundCheck.position + (Vector3.up * 0.1f) + (Vector3.down * groundCheckDistance));
#endif
        // 0.1f is a small offset to start the ray from inside the character
        // it is also good to note that the transform position in the sample assets is at the base of the character
        if (Physics.Raycast(groundCheck.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, groundCheckDistance))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    void Jump()
    {
        playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, m_JumpPower, playerRigidbody.velocity.z);
        isGrounded = false;
    }
}
