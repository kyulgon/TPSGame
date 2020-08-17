using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private PlayerInput playerInput;
    private PlayerShooter playerShooter;
    private Animator animator;

    private Camera followCam;

    public float speed = 6f;
    public float jumpVelocity = 20f;
    [Range(0.01f, 1f)]
    public float airControlPercent; // 공중 조작 필요 변수

    public float speedSmoothTime = 0.1f;
    public float turnSmoothTime = 0.1f;

    private float speedSmoothVelocity;
    private float turnSmoothVelocity;

    private float currentVelocityY;

    public float currentSpeed => new Vector2(characterController.velocity.x, characterController.velocity.z).magnitude;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerShooter = GetComponent<PlayerShooter>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        followCam = Camera.main;
    }

    private void FixedUpdate() // 물리 업데이트
    {
        if (currentSpeed > 0.2f || playerInput.fire ||  playerShooter.aimState == PlayerShooter.AimState.HipFire)
        {
            Rotate();
        }           

        Move(playerInput.moveInput);

        if (playerInput.jump)
        {
            Jump();
        }
           
    }

    void Update()
    {
        UpdateAnimation(playerInput.moveInput);
    }

    public void Move(Vector2 moveInput)
    {
        var targetSpeed = speed * moveInput.magnitude;
        var moveDiection = Vector3.Normalize(transform.forward * moveInput.y + transform.right * moveInput.x);

        // 땅에 닿아있으면 speedSmoothTime을 적용하고 아니면 더 늦게 smoothTime이 적용된다
        var smoothTime = characterController.isGrounded ? speedSmoothTime : speedSmoothTime / airControlPercent;

        // targetSpeed를 Damp 계산
        targetSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, smoothTime);

        currentVelocityY += Time.deltaTime * Physics.gravity.y; // 높이 갱신해주기

        var velocity = moveDiection * targetSpeed + Vector3.up * currentVelocityY;

        characterController.Move(velocity * Time.deltaTime);

        if(characterController.isGrounded) // 바닥에 닿아있면 Y값을 0으로 고정
        {
            currentVelocityY = 0f;
        }
    }

    public void Rotate()
    {
        var targetRotation = followCam.transform.eulerAngles.y;

        targetRotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);

        transform.eulerAngles = Vector3.up * targetRotation;
    }


    public void Jump()
    {
        if (!characterController.isGrounded) return;
        currentVelocityY = jumpVelocity;

    }

    private void UpdateAnimation(Vector2 moveInput)
    {
        var animationSpeedPercent = currentSpeed / speed;

        animator.SetFloat("Vertical Move", moveInput.y * animationSpeedPercent, 0.05f, Time.deltaTime);
        animator.SetFloat("Horizontal Move", moveInput.x * animationSpeedPercent, 0.05f, Time.deltaTime);
    }
}
