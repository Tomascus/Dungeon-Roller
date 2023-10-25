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


    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        StartCoroutine(spawnEnemy(goblinInterval, goblinPrefab));
        StartCoroutine(spawnEnemy(skeletonInterval, skeletonPrefab));
        StartCoroutine(spawnEnemy(eyeInterval, eyePrefab));
        StartCoroutine(spawnEnemy(shroomInterval, shroomPrefab));

    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy) 
    {
        yield return new WaitForSeconds(interval); //waits for interval of enemy spawn
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(spawnRange.x, spawnRange.y), Random.Range(spawnRange.x, spawnRange.y), 0), Quaternion.identity); //Creates enemies in a specified range x,y,z
        EnemyController enemyController = newEnemy.GetComponent<EnemyController>();
        if (enemyController != null)
        {
            enemyController.targetGameobject = player.gameObject;  //Assign the player as the target to follow
            enemyController.playerController = playerController;   
        }
        StartCoroutine(spawnEnemy(interval,enemy));
    }
}
