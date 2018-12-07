using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovementInputProcessor : MonoBehaviour
{
    [SerializeField] float walkMoveStopRadius = .2f;
    [SerializeField] float attackMoveStopRadius = 5f;

    ThirdPersonCharacter cc;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentDestination, clickPoint;

    bool isInDirectControlMode = false; // TODO consider making static later
    private bool jumpPressed;
    bool crouchHeld;

    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        cc = GetComponent<ThirdPersonCharacter>();
        currentDestination = transform.position;
    }

    private void Update()
    {
        if (!jumpPressed) // TODO consider remove jumping
        {
            jumpPressed = Input.GetButtonDown("Jump");
        }
        if (Input.GetKeyDown(KeyCode.G)) // TODO allow player to remap later or auto remap on controller plugin
        {
            isInDirectControlMode = !isInDirectControlMode; // Toggle Mode
            currentDestination = transform.position;
            if (isInDirectControlMode)
            {
                print("Control Method: GamePad");
            }
            else
            {
                print("Control Method: Mouse");
            }
        }
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        crouchHeld = Input.GetKey(KeyCode.C); // TODO consider remove crouching

        if (isInDirectControlMode)
        {
            ProcessDirectMovement();
        }
        else
        {
            ProcessMouseMovementInput();
        }

        jumpPressed = false;
    }

    private void ProcessDirectMovement()
    {
        // read inputs
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // calculate camera relative direction to move:
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveDirection = v * cameraForward + h * Camera.main.transform.right;

        // walk speed multiplier
        if (Input.GetKey(KeyCode.LeftShift)) moveDirection *= .5f;

        // pass all parameters to the character control script
        cc.Move(moveDirection, crouchHeld, jumpPressed);
    }

    private void ProcessMouseMovementInput()
    {
        if (Input.GetMouseButton(0))
        {
            clickPoint = cameraRaycaster.hit.point;
            switch (cameraRaycaster.currentLayerHit)
            {
                case Layer.Walkable:
                    currentDestination = ShortDestination(clickPoint, walkMoveStopRadius);
                    break;
                case Layer.Enemy:
                    currentDestination = ShortDestination(clickPoint, attackMoveStopRadius);
                    break;
                default:
                    print("Unexpected layer here");
                    break;
            }

        }

        WalkToDestination();
    }

    private void WalkToDestination()
    {
        var playerToClickPoint = currentDestination - transform.position; //cc.Move normalizes the move direction
        if (playerToClickPoint.magnitude >= walkMoveStopRadius) // TODO fix when set to >= 0
        {
            cc.Move(playerToClickPoint, crouchHeld, jumpPressed);
        }
        else
        {
            cc.Move(Vector3.zero, crouchHeld, jumpPressed);
        }
    }

    Vector3 ShortDestination(Vector3 destination, float shortening)
    {
        Vector3 reductionVector = (destination - transform.position).normalized * shortening;
        return destination - reductionVector;
    }

    void OnDrawGizmos()
    {
        // Draw movement gizmos;
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, currentDestination);
        Gizmos.DrawSphere(currentDestination, 0.15f);
        Gizmos.DrawSphere(clickPoint, .1f);

        Gizmos.color = new Color(255f, 0f, 0f, .5f);
        Gizmos.DrawWireSphere(transform.position, attackMoveStopRadius);
    }
}

