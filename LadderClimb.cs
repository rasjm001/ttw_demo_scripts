using UnityEngine;

public class PlayerLadderClimb : MonoBehaviour
{
    public float climbSpeed = 3.0f;
    private bool isNearLadder = false;
    private bool isClimbing = false;
    private Transform ladderTransform;
    private Rigidbody rb;
    private CharacterController controller;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Check for "E" key press to start climbing
        if (isNearLadder && Input.GetKeyDown(KeyCode.E))
        {
            ToggleClimb();
        }

        // Handle movement while climbing
        if (isClimbing)
        {
            Climb();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            Debug.Log("is near a ladder");
            isNearLadder = true;
            ladderTransform = other.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            isNearLadder = false;
        }
    }

    void ToggleClimb()
    {
        isClimbing = !isClimbing;
        
        if (isClimbing)
        {
            // Snap player to ladder position (optional)
            transform.position = new Vector3(ladderTransform.position.x, transform.position.y, ladderTransform.position.z);
            rb.useGravity = false; // Disable gravity while climbing
        }
        else
        {
            rb.useGravity = true; // Enable gravity after climbing
        }
    }

    void Climb()
    {
        float verticalInput = Input.GetAxis("Vertical"); // W = 1, S = -1
        Vector3 climbDirection = Vector3.up * verticalInput * climbSpeed * Time.deltaTime;
        controller.Move(climbDirection);
    }
}
