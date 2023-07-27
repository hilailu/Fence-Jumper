using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float bound = -15f;
    private PlayerController player;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }
    void Update()
    {
        if (!player.IsGameOver)
        {
            Vector3 translation = Vector3.left * speed * Time.deltaTime;
            transform.Translate(player.IsDashing ? translation * 2.5f : translation);
        }

        if (transform.position.x < bound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
