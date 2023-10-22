using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBar : MonoBehaviour
{
    //Health bar and making very small adjustments in player and enemy Controller by following this tutorial: https://www.youtube.com/watch?v=IMK5xEluZh0&t=279s&ab_channel=GregDevStuff
    [SerializeField] Transform bar; 

    public void SetState(int current, int max)  //Method for setting bar value between 1 and 0 for red color
    {
        float state = (float)current;
        state /= max;
        if (state < 0f) { state = 0f; } //prevents "spillage" of hp bar when player dies
        bar.transform.localScale = new Vector3(state, 1f, 1f); //transforming x value of bar
    }
}
