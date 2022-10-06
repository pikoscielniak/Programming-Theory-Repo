
using UnityEngine;

// INHERITANCE
public class Bunny : Enemy
{
    [SerializeField] private int damagePower = 10;

    protected override void Attack(Player player)
    {
        player.TakeLive(damagePower);
    }
}