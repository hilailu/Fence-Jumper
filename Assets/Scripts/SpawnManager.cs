using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;
    private PlayerController player;
    private Vector3 spawnPoint = new Vector3(25, 0, 0);
    private float rate = 2f;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObstacle", rate, rate);
    }
    
    void SpawnObstacle()
    {
        GameObject obstacle = obstacles[Random.Range(0, obstacles.Length)];
        if (!player.IsGameOver)
        {
            Instantiate(obstacle, spawnPoint, obstacle.transform.rotation);
        }
    }
}
