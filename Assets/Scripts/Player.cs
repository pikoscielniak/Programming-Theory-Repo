using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speed = 2;
    [SerializeField] float rotationSpeed = 360;

    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var moveVector = new Vector3(horizontal, 0, vertical);

        moveVector.Normalize();

        _rigidbody.AddForce(moveVector * speed, ForceMode.VelocityChange);

        if (moveVector != Vector3.zero)
        {
            var toRotation = Quaternion.LookRotation(moveVector, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation,
                rotationSpeed * Time.deltaTime);
        }
    }
}