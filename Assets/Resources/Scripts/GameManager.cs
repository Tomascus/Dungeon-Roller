using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.SceneManagement;

//From N. Collins class
public class GameManager : MonoBehaviour
{
    private PlayerController player;
    public static GameManager instance;
    [Header("Game States")]
    public bool isPaused;
    public bool isGameOver = false;

    void Awake()
    {
      if(instance == null) 
      {
        instance = this;
        DontDestroyOnLoad(gameObject);
      }
      else{
        Destroy(gameObject);
        return;
      }
    }
    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(isPaused)
            ResumeGame();
            else
            PauseGame();
        } 

        if(Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            LoadMainMenu();
        }

        
        if (!isGameOver)
        {
            // Check if the player object is destroyed
            if (player == null)
            {
                GameOver();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        //this is where youd put your pause menu
    }


    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        //this is where youd turn off your pause menu
    }

    public void GameOver()
    {
        isGameOver = true;
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
