using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHolder : MonoBehaviour
{
    //Continuation of tutorial from: https://www.youtube.com/watch?v=MIt0PJHMN5Y&t=60s&ab_channel=CodeMonkey
    public event EventHandler OnKeysChanged;
    private List<Key.KeyType> keyList;
    private void Awake() {
        keyList = new List<Key.KeyType>(); //Creating list to hold keys
    }

    //List of the keys player is holding
    public List<Key.KeyType> GetKeyList() {
        return keyList;
    }

    //Adding key to the list
    public void AddKey(Key.KeyType keyType) {
        keyList.Add(keyType);
        OnKeysChanged?.Invoke(this, EventArgs.Empty);
    }

    //Removing key from the list
    public void RemoveKey(Key.KeyType keyType) {
        keyList.Remove(keyType);
        OnKeysChanged?.Invoke(this, EventArgs.Empty);
    }

    //Method for checking keys
    public bool ContainsKey(Key.KeyType keyType) {
        return keyList.Contains(keyType);
    }

    //Picks up key when is triggered
    private void OnTriggerEnter2D(Collider2D collider) {
        Key key = collider.GetComponent<Key>();
        if (key != null) {
            AddKey(key.GetKeyType()); //Adds the referenced key from the key method 
            Destroy(key.gameObject); //Destroys the object when picked up
        }

        KeyDoor keyDoor = collider.GetComponent<KeyDoor>(); //On collision with the door
        if (keyDoor != null) {
            if(ContainsKey(keyDoor.GetKeyType())) { //If the player holds the required key to open the door(same color)
            RemoveKey(keyDoor.GetKeyType()); //The key is removed after unlocking the door
            keyDoor.OpenDoor();
            }

    }
}
}
