
// INHERITANCE
public class Grizzly : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

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