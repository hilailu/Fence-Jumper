using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float gravityModifier = 0f;
    [SerializeField] private ParticleSystem _gameOverVfx;
    [SerializeField] private ParticleSystem _runVfx;
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _gameOverSound;
    [SerializeField] private TextMeshProUGUI _scoreText;
    private AudioSource _audioSource;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private bool _isOnGround;
    private bool _canDoubleJump;
    private int _score;
    public bool IsGameOver;
    public bool IsDashing;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
        StartCoroutine(UpdateScore());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) &&
            ((_isOnGround && !IsGameOver) || (!_isOnGround && _canDoubleJump && !IsGameOver)))
        {
            _runVfx.Stop();
            _audioSource.PlayOneShot(_jumpSound);
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            _animator.SetTrigger("Jump_trig");
            _isOnGround = false;
            _canDoubleJump = !_canDoubleJump;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && _isOnGround)
        {
            _animator.speed = 2.5f;
            IsDashing = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && _isOnGround)
        {
            _animator.speed = 1;
            IsDashing = false;
        }
    }

    private IEnumerator UpdateScore()
    {
        while (!IsGameOver)
        {
            _score += 1;
            _scoreText.text = _score.ToString();
            yield return new WaitForSeconds(IsDashing ? 0.2f : 0.5f);
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isOnGround = true;
            _runVfx.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            _audioSource.PlayOneShot(_gameOverSound, 0.5f);
            IsGameOver = true;
            Debug.Log("Game over");
            _animator.SetTrigger("Death_b");
            _animator.SetInteger("DeathType_int", 1);
            _runVfx.Stop();
            _gameOverVfx.Play();
        }
    }
}
