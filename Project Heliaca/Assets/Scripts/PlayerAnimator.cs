using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] private GameInput gameInput;

    private int verticalHash, horizontalHash;
    private void Start()
    {
        verticalHash = Animator.StringToHash("Vertical");
        horizontalHash = Animator.StringToHash("Horizontal");
    }

    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVector();
        animator.SetFloat(verticalHash, inputVector.y);
        animator.SetFloat(horizontalHash, inputVector.x);
    }
}
