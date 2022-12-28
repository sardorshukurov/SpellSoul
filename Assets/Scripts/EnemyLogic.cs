using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyLogic : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform player;
    public Slider healthBar;

    // health of the enemy and its maxDamageIntake
    public int health = 100;
    public int maxDamageIntake = 15;

    private void Start()
    {
        Debug.Log(health);
    }

    void Update()
    {
        if (gameObject.name == "Boss")
        {
            healthBar.value = health;
        }
        // using navmash I made enemy follow the player
        enemy.SetDestination(player.position);

        // if health is less than 0, enemy dies
        if (health <= 0)
        {
            Destroy(gameObject);
            FindObjectOfType<WinScenario>().ReduceNumberOfEnemies();
        }
    }

    // when someone steps on its trigger area
    private void OnTriggerEnter(Collider other)
    {
        // if enemy is near enough. it gets killed
        if (other.gameObject.name == "Player")
        {
            Destroy(other.gameObject);
            FindObjectOfType<GameManager>().GameOver();
        }
    }
}
