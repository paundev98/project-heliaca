using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    private Rigidbody rb;
    private float lifeTime;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        lifeTime = 0;
    }

    private void Start()
    {
        rb.velocity = transform.forward * bulletSpeed;
    }

    private void Update()
    {
        lifeTime += Time.deltaTime;
        if(lifeTime > 2.0f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
