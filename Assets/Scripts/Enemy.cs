using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (IsInRange)
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
    protected abstract bool IsInRange { get; }

    // POLYMORPHISM
    protected abstract void Walk();

    // POLYMORPHISM
    protected abstract void Chase();

    // POLYMORPHISM
    protected abstract void Attack();
}