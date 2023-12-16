using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundObjectHandler : MonoBehaviour
{
    [System.Serializable]
    public class BackgroundObjectSettings
    {
        public GameObject prefab;
        public GameObject parent;
        public List<Texture> sprites;
        public Queue<GameObject> queue = new Queue<GameObject>();
        public float speed;
        public float spawnFrequencyDeviation;
        public float spawnFrequency;
        public float spawnDistance;
        public float despawnDistance;
    }

    public BackgroundObjectSettings cloudSettings;
    public BackgroundObjectSettings mountainSettings;
    public BackgroundObjectSettings treeSettings;

    void Start()
    {
        Invoke(nameof(SpawnTree), 0);
        Invoke(nameof(SpawnMountain), 0);
        Invoke(nameof(SpawnCloud), 0);
    }
    void Update()
    {
        UpdateObjects(cloudSettings);
        UpdateObjects(mountainSettings);
        UpdateObjects(treeSettings);
    }

    #region Invoke proxies
    void SpawnTree()
    {
        SpawnObject(treeSettings);
        float nextSpawnTime = treeSettings.spawnFrequency + (UnityEngine.Random.value * treeSettings.spawnFrequencyDeviation);
        Invoke(nameof(SpawnTree), nextSpawnTime);
    }

    void SpawnMountain()
    {
        SpawnObject(mountainSettings);
        float nextSpawnTime = mountainSettings.spawnFrequency + (UnityEngine.Random.value * mountainSettings.spawnFrequencyDeviation);
        Invoke(nameof(SpawnMountain), nextSpawnTime);
    }

    void SpawnCloud()
    {
        SpawnObject(cloudSettings);
        float nextSpawnTime = cloudSettings.spawnFrequency + (UnityEngine.Random.value * cloudSettings.spawnFrequencyDeviation);
        Invoke(nameof(SpawnCloud), nextSpawnTime);
    }
    #endregion

    // first object instantiation
    void SpawnObject(BackgroundObjectSettings settings)
    {
        // spawn random from texture list
        int randomIndex = UnityEngine.Random.Range(0, settings.sprites.Count);
        GameObject obj = Instantiate(settings.prefab, new Vector3(settings.spawnDistance, UnityEngine.Random.Range(4f, 8f), 0), Quaternion.identity);
        // set parent so that it falls into the canvas and also is organised
        obj.transform.SetParent(settings.parent.transform, false);

        if (obj.tag == "Mountain")
        {
            obj.transform.localScale = new Vector3(80f, 40f, 1f);
        }
        RawImage rawImage = obj.GetComponent<RawImage>();
        if (rawImage != null)
        {
            rawImage.texture = settings.sprites[randomIndex];
        }
        settings.queue.Enqueue(obj);

        // spawn deviation so that objects aren't spawned at the same frequency
        float nextSpawnTime = settings.spawnFrequency + (UnityEngine.Random.value * settings.spawnFrequencyDeviation);
        Invoke(nameof(SpawnObject), nextSpawnTime);
    }

    // movement animation and also deletion of objects out of bounds
    void UpdateObjects(BackgroundObjectSettings settings)
    {
        // move
        foreach (GameObject obj in settings.queue)
        {
            obj.transform.Translate(Vector3.left * settings.speed * Time.deltaTime);
        }

        // delete
        if (settings.queue.Count > 0 && settings.queue.Peek().transform.position.x < -settings.despawnDistance)
        {
            GameObject toRemove = settings.queue.Dequeue();
            Destroy(toRemove);
        }
    }
}
