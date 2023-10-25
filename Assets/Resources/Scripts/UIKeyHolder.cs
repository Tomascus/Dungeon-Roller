using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIKeyHolder : MonoBehaviour
{
    //Continuation of tutorial from: https://www.youtube.com/watch?v=MIt0PJHMN5Y&t=60s&ab_channel=CodeMonkey
    [SerializeField] private KeyHolder keyHolder; //reference to keyholder
    private Transform Container;
    private Transform KeyTemplate;

    private void Awake() {
        Container = transform.Find("Container");
        KeyTemplate = Container.Find("KeyTemplate");
        KeyTemplate.gameObject.SetActive(false);
    }

    private void Start() {
        keyHolder.OnKeysChanged += keyHolder_OnKeysChanged;
    }

    private void keyHolder_OnKeysChanged(object sender, System.EventArgs e) {
        UpdateVisual();
    }

    private void UpdateVisual() {
        //Clean up old keys
        foreach (Transform child in Container) { //cycle through the container
            if(child == KeyTemplate) continue;
            Destroy(child.gameObject); //destroy every key that is not template
        }

        //Instantiate current ky list
        List<Key.KeyType> keyList = keyHolder.GetKeyList(); //We get the list of keys
        for (int i = 0; i < keyList.Count; i++) {
            Key.KeyType keyType = keyList[i];
            Transform keyTransform = Instantiate(KeyTemplate,Container);
            KeyTemplate.gameObject.SetActive(true);
            keyTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-70 * i, 0);
            Image keyImage = keyTransform.Find("Image").GetComponent<Image>();
            switch (keyType) {
                default:
                case Key.KeyType.Red: keyImage.color = Color.red; break;
                case Key.KeyType.Blue: keyImage.color = Color.white; break;
                
            }
        }
    }
}
