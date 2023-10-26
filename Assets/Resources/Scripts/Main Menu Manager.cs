using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//From N. Collins class
//Font used free from: https://www.dafont.com/rahul-parihar.d8946
public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private String gameSceneName = "DungeonLevel";
    
    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
