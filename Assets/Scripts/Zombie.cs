// INHERITANCE

using UnityEngine;

public class Zombie : Enemy
{
    protected override void Attack()
    {
        Debug.Log("Zombie attack");
    }
}