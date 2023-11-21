using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundObjectHandler : MonoBehaviour
{

    public GameObject[] cloudsList;
    public GameObject[] mountainList;
    public GameObject mountainParent;
    public GameObject cloudParent;
    Queue<GameObject> cloudQueue = new Queue<GameObject> ();
    Queue<GameObject> mountainQueue = new Queue<GameObject> ();
    private float cloudSpawnFrequency = 4f;
    private float cloudSpeed = 1f;
    private float mountainSpawnFrequency = 5f;
    private float mountainSpeed = 10f;

    void Start()
    {
        // InvokeRepeating is like Update, begins from 0 and spawns objects according to spawnFrequency
        InvokeRepeating("SpawnCloud", 0, cloudSpawnFrequency);
        InvokeRepeating("SpawnMountain", 0, mountainSpawnFrequency);
    }
    
    void SpawnMountain()
    {
        // spawns a mountain
        int randMount = UnityEngine.Random.Range(0, mountainList.Length);
        GameObject mountain = (Instantiate(mountainList[randMount], new Vector3(30, 0, 0), Quaternion.identity));
        mountain.transform.SetParent(mountainParent.transform, false);
        mountain.transform.Translate(new Vector3(40, 0, 0));
        mountainQueue.Enqueue(mountain);
    }
    void SpawnCloud()
    {
        // Picks a random object from the cloudsList;
        int randCloud = UnityEngine.Random.Range(0, cloudsList.Length);


        // Initialises an object to the scene.
        // selects a cloud from the list according to the random ID
        // sets the height of it (new Vector3) - x,y,z
        // Quaternion.identity is responsible for rotation? idk
        
        // spawns a cloud
        GameObject cloud = (Instantiate(cloudsList[randCloud], new Vector3(12, UnityEngine.Random.Range(4f, 8f), 0), Quaternion.identity));
        cloud.transform.SetParent(cloudParent.transform, false);
        cloud.transform.Translate(new Vector3(10, UnityEngine.Random.Range(4f, 8f), 0));
        cloudQueue.Enqueue(cloud);



    }
    void Update()
    {
        foreach (var cloud in cloudQueue)
        {
            cloud.transform.Translate(Vector3.left * cloudSpeed * Time.deltaTime);
        }
        if (cloudQueue.Count > 0)
        {
            GameObject topCloud = cloudQueue.Peek();
            if (topCloud.transform.position.x < -15f)
            {
                cloudQueue.Dequeue();
                Destroy(topCloud);
            }
        }
        foreach (var cloud in mountainQueue)
        {
            cloud.transform.Translate(Vector3.left * mountainSpeed * Time.deltaTime);
        }
        if (mountainQueue.Count > 0)
        {
            GameObject topMountain = mountainQueue.Peek();
            if (topMountain.transform.position.x < -30f)
            {
                mountainQueue.Dequeue();
                Destroy(topMountain);
            }
        }
    }


}