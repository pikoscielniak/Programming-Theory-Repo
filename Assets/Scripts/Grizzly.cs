
using UnityEngine;

// INHERITANCE
public class Grizzly : Enemy
{
    [SerializeField] private int damagePower = 6;

    protected override void Attack(Player player)
    {
        player.TakeLive(damagePower);
    }
}