using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
// [RequireComponent(typeof(NavMeshAgent))]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] [Range(1, 100)] private float speed;
    [SerializeField] [Range(1, 500)] private float walkRadius;

    private Rigidbody _rigidbody;
    private NavMeshAgent _agent;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
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
        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            _agent.SetDestination(RandomNavMeshLocation());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Attack();
        }
    }

    // POLYMORPHISM
    protected abstract bool PlayerIsInRange { get; }

    // POLYMORPHISM
    protected abstract void Walk();

    // POLYMORPHISM
    protected abstract void Chase();

    // POLYMORPHISM
    protected abstract void Attack();
}