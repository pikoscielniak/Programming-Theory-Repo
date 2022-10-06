// INHERITANCE

using UnityEngine;

public class Bunny : Enemy
{
    protected override void Attack()
    {
        Debug.Log("Bunny attack");
    }
}