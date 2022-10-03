using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    private Rigidbody _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerIsInRange)
        {
            Chase();
        }
        else
        {
            Walk();
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