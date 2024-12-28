using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerFlyBehaviour : MonoBehaviour
{
    [Header("References")]
    [SerializeField] InputActionProperty leftTriggerAction;
    [SerializeField] InputActionProperty rightTriggerAction;
    [SerializeField] InputActionProperty leftStickAction;
    [SerializeField] InputActionProperty rightStickAction;
    [SerializeField] InputActionProperty testFlapAction;
    [SerializeField] Transform leftController;
    [SerializeField] Transform rightController;

    [Header("Settings")]
    [SerializeField] float flapForce = 10;
    [SerializeField] float turbulenceAngle = 15;
    [SerializeField] float turbulenceSpeed = 8;
    [SerializeField] float gravityScale = 0.5f;
    [SerializeField] float glideSpeed = 1;
    [SerializeField] float flyGlideSpeed = 2;
    [SerializeField] float descendSpeed = 5;
    [SerializeField] float turnSpeed = 0.2f;
    [SerializeField] float walkSpeed = 1;
    [SerializeField] bool isDebugMode = true;
    [SerializeField] bool ignoreFlapTrigger = false;
    [SerializeField] float flapForceControllerFactor = 10;

    [Header("Ground")]
    [SerializeField] bool isGrounded = false;
    [SerializeField] float groundOffset = -0.1f;
    [SerializeField] float groundDetection = 0.2f;
    [SerializeField] LayerMask groundLayer;
    float flapForceModified
    {
        get
        {
            if (Player.Instance.Effects.Find(e => e.BlockControl) == null)
                return flapForce * (1 + player.Effects.Sum(e => e.FalpForceModifer));
            else
                return 0;
        }
    }
    Vector3 leftControllerPosition = Vector3.zero;
    Vector3 rightControllerPosition = Vector3.zero;

    Vector3 leftVelocity => leftController.localPosition - leftControllerPosition;
    Vector3 rightVelocity => rightController.localPosition - rightControllerPosition;

    CharacterController characterController;
    Player player;
    Vector3 velocity = Vector3.zero;

    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        player = GetComponent<Player>();

        leftControllerPosition = leftController.localPosition;
        rightControllerPosition = rightController.localPosition;

        if (Debug.isDebugBuild && isDebugMode)
        {
            Camera.main.GetComponent<TrackedPoseDriver>().enabled = false;
        }
    }

    void Update()
    {
        isGrounded = Physics.OverlapSphere(transform.position - Vector3.up * groundOffset, groundDetection, groundLayer).Length > 0;

        // Fly
        float currGlideSpeed = glideSpeed;
        if (Debug.isDebugBuild && isDebugMode)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocity.y = flapForceModified;
                Debug.Log("PlayerFlyBehaviour: Flap by Keyboard");
            }
        }
        else if (testFlapAction.action.triggered)
        {
            if (GetLeftTrigger() || GetRigtTrigger())
                velocity.y = -descendSpeed;
            else
                velocity.y = flapForceModified;
            Debug.Log("PlayerFlyBehaviour: Flap by Controller Button");
        }
        else
        {
            if (ignoreFlapTrigger || GetLeftTrigger() || GetRigtTrigger())
            {
                float leftY = (leftControllerPosition - leftController.localPosition).y - Mathf.Max(0, GetLeftStick().y);
                float rightY = (rightControllerPosition - rightController.localPosition).y - Mathf.Max(0, GetRightStick().y);
                if (leftY > 0)
                    velocity.y += leftY * flapForceModified * flapForceControllerFactor;
                if (rightY > 0)
                    velocity.y += rightY * flapForceModified * flapForceControllerFactor;

                currGlideSpeed += (leftControllerPosition - leftController.localPosition).z * flyGlideSpeed;
            }
        }

        leftControllerPosition = leftController.localPosition;
        rightControllerPosition = rightController.localPosition;

        // Switch Mouse
        if (Debug.isDebugBuild && isDebugMode)
            if (Input.GetKeyDown(KeyCode.Tab))
                Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;

        // Walk & Rotate
        float inputH = 0;
        float inputV = 0;
        if (Debug.isDebugBuild && isDebugMode)
        {
            inputH = Input.GetAxis("Horizontal");
            inputV = Input.GetAxis("Vertical");
        }
        else
        {
            inputH = Mathf.Min((GetLeftStick().x + GetRightStick().x), 1);
            inputV = Mathf.Min((GetLeftStick().y + GetRightStick().y), 1);
        }

        if (inputH != 0)
            transform.Rotate(Vector3.up * inputH * turnSpeed);

        Vector3 moveDirection = Vector3.zero;
        if (inputV != 0 && Mathf.Abs(inputH) <= 0.5f)
            moveDirection += transform.forward * inputV * walkSpeed;

        if (isGrounded)
            characterController.Move(moveDirection * Time.deltaTime);
        else
        {
            moveDirection += transform.forward * currGlideSpeed;
            characterController.Move(moveDirection * Time.deltaTime);
        }

        // Turbulence
        if (Player.Instance.Effects.Find(e => e.HasTurbulence) != null)
            transform.Rotate(Vector3.up * Mathf.Sin(Time.time * turbulenceSpeed) * turbulenceAngle * Time.deltaTime);

        // Simulator Camerra
        if (Debug.isDebugBuild && isDebugMode)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            Vector3 cameraAngles = Camera.main.transform.eulerAngles;
            float newVerticalRotation = cameraAngles.x - mouseY;
            if (newVerticalRotation > 180f) newVerticalRotation -= 360f;
            newVerticalRotation = Mathf.Clamp(newVerticalRotation, -90f, 90f);
            float newHorizontalRotation = cameraAngles.y + mouseX;
            Camera.main.transform.eulerAngles = new Vector3(newVerticalRotation, newHorizontalRotation, 0f);
        }

        // Modifiers
        Vector3 velocityModifier = Vector3.zero;
        foreach (var effect in Player.Instance.Effects)
            velocityModifier += effect.VelocityModifier;
        velocity += velocityModifier * Time.deltaTime;
        velocity.y += Physics.gravity.y * Time.deltaTime * gravityScale;
        characterController.Move(velocity * Time.deltaTime);
    }

    Vector2 GetLeftStick()
    {
        return leftStickAction.action.ReadValue<Vector2>();
    }

    Vector3 GetRightStick()
    {
        return rightStickAction.action.ReadValue<Vector2>();
    }

    bool GetLeftTrigger()
    {
        return leftTriggerAction.action.ReadValue<float>() > 0.5f;
    }

    bool GetRigtTrigger()
    {
        return rightTriggerAction.action.ReadValue<float>() > 0.5f;
    }
}
