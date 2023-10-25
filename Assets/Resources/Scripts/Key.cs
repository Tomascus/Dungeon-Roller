using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    //For this key script I followed tutorial from: https://www.youtube.com/watch?v=MIt0PJHMN5Y&t=60s&ab_channel=CodeMonkey
    [SerializeField] private KeyType keyType;
    public enum KeyType { //Stores key types to choose from
        Red,
        Blue
    }

    //returns keytype (blue, red...) to the keyHolder script
    public KeyType GetKeyType() {
        return keyType;
    }
}
