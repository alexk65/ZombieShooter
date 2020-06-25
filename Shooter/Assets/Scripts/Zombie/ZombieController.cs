using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    public int health = 100;
    public Transform attackPosition;
    public float attackRange = 1.1f;
    public int damage = 10;
    public float attackCooldown = 2f;

    private float timePassedAfterAttack;
    private Animator animator;
    private NavMeshAgent navMeshAgent;

    public bool IsAlive { get; set; }

    public void ReactToHit(int damage)
    {
        TakeDamage(damage);
    }

    private void Start()
    {
        IsAlive = true;
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        if (IsAlive)
        {
            MoveToPlayer();

            Collider[] playerColliders = Physics.OverlapSphere(attackPosition.position, attackRange);
            foreach (var playerCollider in playerColliders)
            {
                var playerController = playerCollider.GetComponent<PlayerController>();
                if (playerController != null)
                {
                    AttackPlayer(playerController);
                }
            }
        }
        else
        {
            navMeshAgent.isStopped = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(attackPosition.position, attackRange);
    }

    private void AttackPlayer(PlayerController playerController)
    {
        if (timePassedAfterAttack <= 0)
        {
            animator.SetBool("Attack", true);
            playerController.TakeDamage(damage);
            timePassedAfterAttack = attackCooldown;
        }
        else
        {
            timePassedAfterAttack -= Time.deltaTime;
        }
    }

    private void MoveToPlayer()
    {
        var player = GameObject.FindWithTag("Player");
        /*transform.LookAt(player.transform);*/
        if (player != null)
        {
            navMeshAgent.destination = player.transform.position;
        }
    }


    private void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        IsAlive = false;
        animator.SetBool("Dead", !IsAlive);

        yield return new WaitForSeconds(2);

        Destroy(gameObject);
    }
}
