using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 0f;

    [SerializeField]
    float maxSpeed = 20f;

    [SerializeField]
    float jumpForce = 5f;

    [SerializeField]
    Score score;

    [SerializeField]
    ScoreP2 scoreP2;

    [SerializeField]
    Vector3 rayOrigin;

    [SerializeField]
    float rayDistance = 5f;

    [SerializeField]
    LayerMask groundLayer;

    [SerializeField]
    Color rayColor;

    [SerializeField]
    bool useGravity = true;

    [SerializeField]
    float gravityValue = 1f;

    [SerializeField]
    Camera cameraPlayer;

    
    Rigidbody rb;
    private Vector2 movementInput = Vector2.zero;
    private bool jumped = false;

    

    void Awake() 
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        //startoooo
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        jumped = context.action.triggered;
        if(jumped && IsGrounding)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void Update()
    {

        
        if(rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
    }

    private void FixedUpdate() 
    {
        Move();
        rb.useGravity = false;
        if (useGravity) rb.AddForce(Physics.gravity * gravityValue * rb.mass);
    }

    

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("soul"))
        {
            if(gameObject.tag == "player1")
            {
                Soul soul = other.GetComponent<Soul>();
                score.AddPoints(soul.GetPoints);
                Destroy(other.gameObject);
            }else
            {
                Soul soul = other.GetComponent<Soul>();
                scoreP2.AddPointsP2(soul.GetPoints);
                Destroy(other.gameObject);
            }
            
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = rayColor;
        Gizmos.DrawRay(transform.position + rayOrigin, Vector3.down * rayDistance);
    }
    
    Vector3 Axis => new Vector3(movementInput.x, 0f, movementInput.y);
    //bool JumpButton => Input.GetButtonDown("Jump");
    bool IsGrounding => Physics.Raycast(transform.position + rayOrigin, Vector3.down, rayDistance, groundLayer);

    void Move() 
    {
        
        rb.AddForce(cameraPlayer.transform.TransformDirection(Axis) * moveSpeed);
        
        
        
        //Camera.allCameras Camera.main.transform.TransformDirection(Axis) Axis.Normalize();
        Axis.Normalize();
    }
}