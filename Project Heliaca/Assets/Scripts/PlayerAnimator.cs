using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Player player;

    private int walkingHash, aimingHash, deadHash;
    private void Start()
    {
        deadHash = Animator.StringToHash("isDead");
        walkingHash = Animator.StringToHash("isWalking");
        aimingHash = Animator.StringToHash("isAiming");
    }

    private void Update()
    {
        bool isDead = player.IsDying();
        bool isWalking = player.IsWalking();
        bool isAiming = player.IsAiming();
        animator.SetBool(walkingHash, isWalking);
        animator.SetBool(aimingHash, isAiming);
        animator.SetBool(deadHash, isDead);
    }
}
