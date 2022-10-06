
using UnityEngine;

// INHERITANCE
public class Zombie : Enemy
{
    [SerializeField] private int damagePower = 5;

    protected override void Attack(Player player)
    {
        player.TakeLive(damagePower);
    }
}