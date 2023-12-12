using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundObjectHandler : MonoBehaviour
{

    public GameObject cloudObject;
    public GameObject cloudParent;
    Queue<GameObject> cloudQueue = new Queue<GameObject> ();
    public List<Texture> cloudSprites = new List<Texture> ();
    
    public GameObject mountainObject;
    public GameObject mountainParent;
    Queue<GameObject> mountainQueue = new Queue<GameObject> ();
    public List<Texture> mountainSprites = new List<Texture>();

    public GameObject treeParent;
    public GameObject treeObject;
    Queue<GameObject> treeQueue = new Queue<GameObject> ();
    public List<Texture> treeSprites = new List<Texture> ();
    
    private float cloudSpeed = 0.5f;
    private float mountainSpawnFrequency = 120f;
    private float mountainSpeed = 0.02f;
    private float treeSpeed = 0.125f;


    void Start()
    {
        gameObject.GetComponent<WallpaperHandler>().WallpaperUpdate();
        // InvokeRepeating is like Update, begins from 0 and spawns objects according to spawnFrequency
        Invoke("SpawnCloud",0);
        Invoke("SpawnTree",0);
        Invoke("SpawnMountain",0);
        //GameObject mountain = (Instantiate(mountainObject, new Vector3(0, 0, 0), Quaternion.identity));
        //mountain.transform.SetParent(mountainParent.transform, false);
        //mountain.transform.Translate(new Vector3(4, 0, 0));
        //mountainQueue.Enqueue(mountain);
        //GameObject tree = (Instantiate(treeObject, new Vector3(0, 0, 0), Quaternion.identity));
        //tree.transform.SetParent(treeParent.transform, false);
        //tree.transform.Translate(new Vector3(0, 0, 0));
        //treeQueue.Enqueue(tree);
    }

    void SpawnMountain()
    {
        // spawns a mountain
        int randMountain = UnityEngine.Random.Range(0, mountainSprites.Count);

        // spawns a cloud
        GameObject mountain = (Instantiate(mountainObject, new Vector3(30, 0, 0), Quaternion.identity));
        mountain.transform.SetParent(mountainParent.transform, false);
        mountain.transform.Translate(new Vector3(10, UnityEngine.Random.Range(0f, 0.1f), 0));
        // Change the sprite of the cloud
        RawImage rawImage = mountain.GetComponent<RawImage>();
        if (rawImage != null)
        {
            rawImage.texture = mountainSprites[randMountain];
        }
        mountainQueue.Enqueue(mountain);
        Invoke("SpawnMountain", UnityEngine.Random.Range(120f, 150f));
    }
    void SpawnTree()
    { 
        // Picks a random object from the cloudsList;
        int randTree = UnityEngine.Random.Range(0, treeSprites.Count);


        // Initialises an object to the scene.
        // selects a cloud from the list according to the random ID
        // sets the height of it (new Vector3) - x,y,z
        // Quaternion.identity is responsible for rotation? idk

        // spawns a cloud
        GameObject tree = (Instantiate(treeObject, new Vector3(12, UnityEngine.Random.Range(4f, 8f), 0), Quaternion.identity));
        tree.transform.SetParent(treeParent.transform, false);
        tree.transform.Translate(new Vector3(10, UnityEngine.Random.Range(0f, 0.1f), 0));
        // Change the sprite of the cloud
        RawImage rawImage = tree.GetComponent<RawImage>();
        if (rawImage != null)
        {
            rawImage.texture = treeSprites[randTree];
        }
        treeQueue.Enqueue(tree);
        Invoke("SpawnTree", UnityEngine.Random.Range(20f, 80f));
    }
    void SpawnCloud()
    {
        // Picks a random object from the cloudsList;
        int randCloud = UnityEngine.Random.Range(0, cloudSprites.Count);


        // Initialises an object to the scene.
        // selects a cloud from the list according to the random ID
        // sets the height of it (new Vector3) - x,y,z
        // Quaternion.identity is responsible for rotation? idk
        
        // spawns a cloud
        GameObject cloud = (Instantiate(cloudObject, new Vector3(12, UnityEngine.Random.Range(4f, 8f), 0), Quaternion.identity));
        cloud.transform.SetParent(cloudParent.transform, false);
        cloud.transform.Translate(new Vector3(10, UnityEngine.Random.Range(4f, 8f), 0));
        // Change the sprite of the cloud
        RawImage rawImage = cloud.GetComponent<RawImage>();
        if (rawImage != null)
        {
            rawImage.texture = cloudSprites[randCloud];
        }
        cloudQueue.Enqueue(cloud);
        Invoke("SpawnCloud", UnityEngine.Random.Range(4f, 16f));
    }
    void Update()
    {
        // move clouds
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
        // move mountains
        foreach (var mountain in mountainQueue)
        {
            mountain.transform.Translate(Vector3.left * mountainSpeed * Time.deltaTime);
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
        
        // move trees
        foreach (var tree in treeQueue)
        {
            tree.transform.Translate(Vector3.left * treeSpeed * Time.deltaTime);
        }
        if (treeQueue.Count > 0)
        {
            GameObject topTree = treeQueue.Peek();
            if (topTree.transform.position.x < -15f)
            {
                treeQueue.Dequeue();
                Destroy(topTree);
            }
        }
    }


}