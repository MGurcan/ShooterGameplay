using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameData gameData;

    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public float spawnInterval = 30f;

    [SerializeField] private Transform playerTransform;

    [SerializeField]
    private float enemyHealth;

    public GameManager gameManager;
    private void Start()
    {
        StartCoroutine(SpawnEnemies());

        if (gameData.selectedDifficulty == "Easy")
            spawnInterval = 30f;
        else if (gameData.selectedDifficulty == "Medium")
            spawnInterval = 25f;
        else if (gameData.selectedDifficulty == "Hard")
            spawnInterval = 20f;
    }

    private IEnumerator SpawnEnemies()
    {
        if(gameManager.currentEnemy < gameManager.totalEnemy)
        {
            while (true)
            {
                GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
                newEnemy.GetComponent<NavMeshAgent>().destination = playerTransform.position;
                newEnemy.GetComponent<EnemyMovement>().SetHealth(enemyHealth);

                gameManager.EnemyCreated();

                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }
}
