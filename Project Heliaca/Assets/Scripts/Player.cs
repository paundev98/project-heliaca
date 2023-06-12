using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int hp;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Camera _main;
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private Image hpbar;

    [SerializeField] private Transform shootingPosition;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private float fireRate;
    [SerializeField] private float walkingRotation;
    [SerializeField] private float aimingRotation;

    private float fireCooldown = 0f;

    private Vector2 inputVector;
    private bool isWalking;
    private bool isAiming;
    private bool isShooting;
    private bool isDying = false;
    private int startingHp;

    private void Start()
    {
        startingHp = hp;
    }
    private void Update()
    {
        HandlePhisics();
    }

    private void HandlePhisics()
    {
        if (isDying)
            return;

        inputVector = gameInput.GetMovementVector().normalized;
        isAiming = gameInput.IsAiming() == 1;
        isWalking = inputVector != Vector2.zero;

        isShooting = gameInput.IsShooting() == 1;

        if (isWalking && !isAiming)
        {
            playerPrefab.transform.localRotation = Quaternion.Euler(new Vector3(0f, walkingRotation, 0f));

            Vector3 targetDirection = new Vector3(inputVector.x, 0f, inputVector.y);
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            Vector3 moveDirection = transform.forward * moveSpeed * Time.deltaTime;
            transform.position += moveDirection;
        }

        if (isAiming)
        {
            playerPrefab.transform.localRotation = Quaternion.Euler(new Vector3(0f, aimingRotation, 0f));
            Vector3 mousePosition = gameInput.GetMouseWorldPosition();
            Vector3 direction = (new Vector3(mousePosition.x, transform.position.y, mousePosition.z) - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

            if (isShooting)
                Shoot();
        }

        _main.transform.position = transform.position + cameraOffset;

        if (Input.GetKeyUp(KeyCode.Escape))
            Quit();
    }

    private void Shoot()
    {
        if (fireCooldown <= 0f)
        {
            Instantiate(bulletPrefab, shootingPosition.position, shootingPosition.rotation * Quaternion.Euler(90, 0, 0));
            fireCooldown = 1f / fireRate;
        }
        else
            fireCooldown -= Time.deltaTime;
    }

    public bool IsWalking() { return isWalking; }
    public bool IsAiming() { return isAiming; }

    public bool IsDying() { return isDying; }
    public void Quit()
    {
        Application.Quit();
    }

    private void OnTriggerEnter(Collider other)
    {
        hp--;
        hpbar.fillAmount = (float)hp / startingHp;

        if (hp <= 0)
        {
            isDying = true;
        }
    }

    public void IncreaseHP()
    {
        hp++;
        hpbar.fillAmount = (float)hp / startingHp;
    }
}