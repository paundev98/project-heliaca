using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float chaseDistance;
    [SerializeField] float attackRange;
    [SerializeField] int hp;

    [SerializeField] private Transform shootingPosition;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Image hpBar;

    private float fireCooldown = 0f;
    private Vector3 startingPosition;
    private bool shouldFire = false;
    private bool isWalking = true;
    private bool isDying = false;
    private bool isAiming = false;
    private int startingHp;

    private GameObject player;
    private Player playerComponent;
    private Vector3 playerPosition;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerComponent = player.GetComponent<Player>();
        playerPosition = player.transform.position;
        startingHp = hp;
        agent.SetDestination(playerPosition);
        startingPosition = transform.position;
        Canvas canvas = gameObject.GetComponentInChildren<Canvas>();
        canvas.worldCamera = Camera.main;
    }

    private void Update()
    {
        if (isDying)
        {
            agent.enabled = false;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            return;
        }

        playerPosition = player.transform.position;


        float distance = Vector3.Distance(transform.position, playerPosition);

        if (distance < attackRange)
            shouldFire = true;
        else
            shouldFire = false;

        if (distance <= chaseDistance)
        {
            agent.SetDestination(playerPosition);
            Vector3 direction = (playerPosition - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 40);
        }

        if (transform.position == startingPosition)
            isWalking = false;
        else
            isWalking = true;

        if (shouldFire)
        {
            Shoot();
        }
    }
    public bool IsWalking() { return isWalking; }
    public bool IsDying() { return isDying; }
    public bool IsAiming() { return isAiming; }

    private void Shoot()
    {
        if (fireCooldown <= 0f)
        {
            Instantiate(bulletPrefab, shootingPosition.position, shootingPosition.rotation * Quaternion.Euler(90f, 0f, 0f));
            fireCooldown = 1f / fireRate;
        }
        else
            fireCooldown -= Time.deltaTime;
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        hp--;
        hpBar.fillAmount = (float)hp / startingHp;
        if (hp <= 0)
        {
            Invoke("DestroyEnemy", 5.0f);
            playerComponent.IncreaseHP();
            isDying = true;
        }
    }
}
