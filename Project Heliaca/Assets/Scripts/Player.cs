using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Camera _main;
    [SerializeField] private Vector3 cameraOffset;

    [SerializeField] private Transform shootingPosition;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int bulletsPerSecond;

    private float fireRate;
    private float fireTime;

    private Vector2 inputVector;
    private Vector3 lastInputPostion;

    private void Start()
    {
        lastInputPostion = Vector3.zero;
        fireRate = 1 / bulletsPerSecond;
        fireTime = 0;
    }
    private void Update()
    {
        inputVector = gameInput.GetMovementVector().normalized;
        var (success, position) = gameInput.GetMousePosition();

        transform.position += transform.forward * moveSpeed * inputVector.y * Time.deltaTime;
        transform.position += transform.right * moveSpeed * inputVector.x * Time.deltaTime;
        _main.transform.position = transform.position + cameraOffset;

        if(success && lastInputPostion != position)
        {
            Vector3 direction = position - transform.position;
            direction.y = 0;
            transform.forward = direction;
            lastInputPostion = position;
        }

        bool isShooting = gameInput.IsShooting() == 1;


        if (isShooting)
            Shoot(position);

    }

    private void Shoot(Vector3 mousePos)
    {
        fireTime += Time.deltaTime;
        if (fireTime >= fireRate)
        {
            Vector3 aimDir = (mousePos - shootingPosition.transform.position).normalized;
            Instantiate(bulletPrefab, shootingPosition.transform.position, Quaternion.LookRotation(aimDir, Vector3.up));
            fireTime = 0;
        }
    }

}