using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInputs), typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInputs playerInput;
    private StateMachine _playerStateMachine;

    [Header("Movement")] 
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float accelerator = 10;
    [SerializeField] private float jumpHeight = 1.25f;
    [SerializeField] private float gravity = -15f;
    [SerializeField] private float jumpTimeout = 0.1f;

    [SerializeField] private bool isGrounded = true;
    [SerializeField] private float groundedRadius = 0.5f;
    [SerializeField] private float groundedOffset = 0.15f;
    [SerializeField] private LayerMask groundLayer;

    public CharacterController CharController =>  _charController;

    public bool IsGrounded => isGrounded;

    public StateMachine PlayerStateMachine => _playerStateMachine;

    private CharacterController _charController;
    private float targetSpeed;
    private float verticalVelocity;
    private float jumpCooldown;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInputs>();
        _charController = GetComponent<CharacterController>();
        _playerStateMachine = new StateMachine(this);
    }

    private void Start()
    {
        _playerStateMachine.Initialize(_playerStateMachine.idleState);
    }

    private void Update()
    {
        _playerStateMachine.Update();
    }

    private void LateUpdate()
    {
        CalculateVertical();
        Move();
    }

    private void Move()
    {
        Vector3 inputVector = playerInput.InputVector;

        if (inputVector == Vector3.zero)
        {
            targetSpeed = 0;
        }

        float currentHorizontalSpeed =
            new Vector3(_charController.velocity.x, 0.0f, _charController.velocity.z).magnitude;
        float tolerance = 0.1f;

        if (currentHorizontalSpeed < targetSpeed - tolerance || currentHorizontalSpeed > targetSpeed + tolerance)
        {
            targetSpeed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed, Time.deltaTime * accelerator);
            targetSpeed = Mathf.Round(targetSpeed * 1000f) / 1000f;
        }
        else
        {
            targetSpeed = moveSpeed;
        }

        _charController.Move((inputVector.normalized * targetSpeed * Time.deltaTime) +
                             new Vector3(0f, verticalVelocity, 0f) * Time.deltaTime);
    }

    private void CalculateVertical()
    {
        if (isGrounded)
        {
            if (verticalVelocity < 0f)
            {
                verticalVelocity = -2f;
            }

            if (playerInput.IsJumping && jumpCooldown <= 0f)
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            if (jumpCooldown >= 0f)
            {
                jumpCooldown -= Time.deltaTime;
            }
        }
        else
        {
            jumpCooldown = jumpTimeout;
            playerInput.IsJumping = false;
        }

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y + groundedOffset,
            transform.position.z);
        isGrounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayer, QueryTriggerInteraction.Ignore);
    }
}
