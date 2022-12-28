using UnityEngine;

public class PlayerProjectileLogic : MonoBehaviour
{
    float life = .5f;
    public EnemyLogic enemy;

    private void Start()
    {
        Destroy(gameObject, life);
    }

    void OnTriggerEnter(Collider other)
    {
        // if it collides with basicenemy, it reduces its helath randomly and destroys self
        if (other.gameObject.name == "BasicEnemy" || other.gameObject.name == "Boss")
        {
            EnemyLogic enemy = other.gameObject.GetComponent<EnemyLogic>();
            Debug.Log(enemy.health);
            enemy.health -= Random.Range(0, enemy.maxDamageIntake);
            Debug.Log(enemy.health);
            FindObjectOfType<Score>().AddScore();
        }
        Destroy(gameObject);
    }
}
