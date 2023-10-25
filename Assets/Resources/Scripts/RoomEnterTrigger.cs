using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEnterTrigger : MonoBehaviour
{
    [SerializeField] private GameObject openDoorPrefab1;
    [SerializeField] private GameObject closedDoorPrefab1;
    [SerializeField] private GameObject openDoorPrefab2;
    [SerializeField] private GameObject closedDoorPrefab2;
    public EnemySpawner enemySpawner; //Reference to enemy spawner script
    private bool hasTriggered = false;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
           
            ReplaceDoor(); //Replace open doors with a closed door asset
            StartCoroutine(ReactivateDoors());
            

            //Start the coroutine to bring back the original doors
            enemySpawner.StartCoroutine(enemySpawner.SpawnEnemies()); //Activate enemy spawner
            hasTriggered = true; //Only makes it trigger once per room
        }
    }

    void ReplaceDoor()
    {
        openDoorPrefab1.SetActive(false);   //Deactivate the open door
        closedDoorPrefab1.SetActive(true);  //Activate the closed door
        openDoorPrefab2.SetActive(false);   //Same for the second pair
        closedDoorPrefab2.SetActive(true);  
    }

    IEnumerator ReactivateDoors()
    {
        yield return new WaitForSeconds(30f);

        while (!enemySpawner.NoEnemiesLeft())
    {
        yield return null; //Wait until all enemies are defeated
    }
        closedDoorPrefab1.SetActive(false);  //Deactivate the closed door
        openDoorPrefab1.SetActive(true);     //Activate the open door
        closedDoorPrefab2.SetActive(false);  //Same for the second pair
        openDoorPrefab2.SetActive(true);     
        
    }
}
