using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speed = 2;
    [SerializeField] float rotationSpeed = 360;
    [SerializeField] private int initHealth = 100;
    [SerializeField] private TextMeshProUGUI healthText;
    private GameManager _gameManager;

    private int _health;

    private Rigidbody _rigidbody;

    void Start()
    {
        _gameManager = GameManager.Get;
        _health = initHealth;
        _rigidbody = GetComponent<Rigidbody>();
        SetHealthText();
    }

    private void SetHealthText()
    {
        healthText.text = $"Health: {_health}";
    }

    // Update is called once per frame
    void Update()
    {
        if (!_gameManager.IsGameOn)
        {
            return;
        }

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

    public bool IsDead => _health <= 0;

    public void TakeLive(int howMuch)
    {
        if (IsDead)
        {
            return;
        }

        _health -= howMuch;
        if (_health < 0)
        {
            _health = 0;
        }

        if (IsDead)
        {
            _gameManager.GameLost();
        }

        SetHealthText();
    }

    public void StartNewGame()
    {
        _health = initHealth;
        SetHealthText();
    }
}