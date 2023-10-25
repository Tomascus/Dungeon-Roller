using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    //Continuation of tutorial from: https://www.youtube.com/watch?v=MIt0PJHMN5Y&t=60s&ab_channel=CodeMonkey
    [SerializeField] private Key.KeyType keyType;

        //Return the type of key required to open the door
    public Key.KeyType GetKeyType() {
        return keyType;
    }

    public void OpenDoor() {
        gameObject.SetActive(false);
    }
}
