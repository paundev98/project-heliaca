using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Enemy enemy;

    private int walkingHash, aimingHash, deathHash;
    private void Start()
    {
        walkingHash = Animator.StringToHash("isWalking");
        aimingHash = Animator.StringToHash("isAiming");
        deathHash = Animator.StringToHash("isDying");
    }

    private void Update()
    {
        bool isWalking = enemy.IsWalking();
        animator.SetBool(walkingHash, isWalking);
        animator.SetBool(aimingHash, isWalking);
        animator.SetBool(deathHash, isWalking);
    }
}
