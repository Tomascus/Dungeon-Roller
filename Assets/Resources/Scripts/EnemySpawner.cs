using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //Based on the tutorial with changes: https://www.youtube.com/watch?v=SELTWo1XZ0c&t=3s&ab_channel=ModdingbyKaupenjoe
    [SerializeField] private GameObject goblinPrefab;
    public float goblinInterval = 10f;
    [SerializeField] private GameObject skeletonPrefab;
    public float skeletonInterval = 15f;
    [SerializeField] private GameObject eyePrefab;
    public float eyeInterval = 8f;
    [SerializeField] private GameObject shroomPrefab;
    public float shroomInterval = 15f;
    [SerializeField] private Transform player; //Getting player ingame
    public PlayerController playerController; //Referencing playercontroller
    public Vector3 spawnRange = new Vector3(-5f, 5f, 0); //For setting different spawn ranges
    private bool Spawning = true;
    public int enemyCount = 0; //Track the number of enemies
    
    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }
    [HideInInspector] public IEnumerator SpawnEnemies()
    {
        while (Spawning)
        {
            StartCoroutine(spawnEnemy(goblinInterval, goblinPrefab));
            StartCoroutine(spawnEnemy(skeletonInterval, skeletonPrefab));
            StartCoroutine(spawnEnemy(eyeInterval, eyePrefab));
            StartCoroutine(spawnEnemy(shroomInterval, shroomPrefab));
            yield return new WaitForSeconds(30f); //30 second timer for spawning
            Spawning = false; //Stops spawning enemies
        }
    }

    public void EnemySpawned()
    {
        enemyCount++; //Adds 1 to the number of enemies
    }

    public void EnemyDefeated()
    {
        enemyCount--; //Removes 1 from the number of enemies
    }

    public bool NoEnemiesLeft()
    {
        return enemyCount == 0; //Check if all enemies are defeated
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy) 
    {
        while (Spawning) { //Checks if the enemies are still spawning
        yield return new WaitForSeconds(interval); //waits for interval of enemy spawn
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(spawnRange.x, spawnRange.y), Random.Range(spawnRange.x, spawnRange.y), 0), Quaternion.identity); //Creates enemies in a specified range x,y,z
        EnemyController enemyController = newEnemy.GetComponent<EnemyController>();
        if (enemyController != null)
        {
            enemyController.targetGameobject = player.gameObject;  //Assign the player as the target to follow
            enemyController.playerController = playerController;   
            EnemySpawned();
        }
        }
       
    }
}
