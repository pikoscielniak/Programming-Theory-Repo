// INHERITANCE

public class Zombie : Enemy
{

    protected override bool PlayerIsInRange { get; }

    protected override void Walk()
    {
        throw new System.NotImplementedException();
    }

    protected override void Chase()
    {
        throw new System.NotImplementedException();
    }

    protected override void Attack()
    {
        throw new System.NotImplementedException();
    }
}