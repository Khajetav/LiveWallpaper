using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundSelector : MonoBehaviour
{
    public GameObject[] objectsToChange;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Animated")
        {
            foreach (GameObject obj in objectsToChange)
            {
                Debug.Log("Entering foreach");
                string name = "Day" + "Background";
                Debug.Log(name + " is the name");
                SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = Resources.Load<Sprite>("Art/Backgrounds/" + name);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
