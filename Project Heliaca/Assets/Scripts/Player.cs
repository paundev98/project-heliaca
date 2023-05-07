using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Animator animator;

    private bool isWalking;
    private bool isRotating;

    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Quaternion targetRotation = gameInput.GetMouseRotationInWorld();
        Vector3 moveDir = new Vector3(inputVector.x, 0.0f, inputVector.y);

        transform.position += moveDir * moveSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
        isWalking = moveDir != Vector3.zero;
        isRotating = targetRotation != Quaternion.identity;

        if (isWalking)
            animator.SetBool("isWalking", true);
        else
            animator.SetBool("isWalking", false);
    }

    public bool IsWalking() { return isWalking; }
    public bool IsRotating() { return isRotating; }
}
