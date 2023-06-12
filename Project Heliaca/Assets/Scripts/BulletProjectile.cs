using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    private float lifeTime;
    private void Awake()
    {
        lifeTime = 0;
    }

    private void Update()
    {
        lifeTime += Time.deltaTime;
        if(lifeTime > 2.0f)
            Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        transform.position += transform.up * bulletSpeed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
