using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] [Range(1, 100)] private float speed;
    [SerializeField] [Range(1, 500)] private float walkRadius;
    [SerializeField] private Player player;
    [SerializeField] private float fieldOfViewRadius = 1f;

    private NavMeshAgent _agent;
    private float _waitTimeBetweenAttacks = 1f;
    private Coroutine _attackCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = speed;
        _agent.SetDestination(RandomNavMeshLocation());
    }

    private Vector3 RandomNavMeshLocation()
    {
        var finalPosition = Vector3.zero;
        var randomPosition = Random.insideUnitSphere * walkRadius;
        randomPosition += transform.position;
        if (NavMesh.SamplePosition(randomPosition, out var hit, walkRadius, 1))
        {
            finalPosition = hit.position;
        }

        return finalPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlayerInView)
        {
            WalkTowardsPlayer();
        }
        else
        {
            Wander();
        }
    }

    private void Wander()
    {
        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            _agent.SetDestination(RandomNavMeshLocation());
        }
    }

    private void WalkTowardsPlayer()
    {
        _agent.SetDestination(player.transform.position);
    }

    // ENCAPSULATION
    private bool IsPlayerInView => (player.transform.position - transform.position).magnitude < fieldOfViewRadius;

    private void StartAttacking()
    {
        _attackCoroutine = StartCoroutine(DoAttack());
    }

    private IEnumerator DoAttack()
    {
        while (true)
        {
            Attack(player);
            yield return new WaitForSeconds(_waitTimeBetweenAttacks);
        }
        // ReSharper disable once IteratorNeverReturns coroutine is killed
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsPlayer(collision))
        {
            StartAttacking();
        }
    }

    private static bool IsPlayer(Collision collision)
    {
        return collision.gameObject.CompareTag("Player");
    }

    private void OnCollisionExit(Collision other)
    {
        if (IsPlayer(other))
        {
            StopCoroutine(_attackCoroutine);
        }
    }

    // POLYMORPHISM
    protected abstract void Attack(Player player);
}