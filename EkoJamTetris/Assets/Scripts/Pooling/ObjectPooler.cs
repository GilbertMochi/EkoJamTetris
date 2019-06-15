using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool//class for all different types of objects we want to make a pool out of
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;//list to hold the different types of objects to make pools out of
    public Dictionary<string, Queue<GameObject>> PoolDictionary;//actual object pool

    public static ObjectPooler Instance;//pseudo singleton
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool p in pools)//loop through each pool object in the list
        {
            Queue<GameObject> pool = new Queue<GameObject>();//place holder queue for holding the objects

            for (int i = 0; i < p.size; i++)//loop through as many times as size in the pool object
            {
                GameObject obj = Instantiate(p.prefab);//create a new object
                obj.SetActive(false);//make it inactive
                pool.Enqueue(obj);//add to the placeholder queue
            }

            //add the place holder queue to the final pool dictionary
            PoolDictionary.Add(p.tag, pool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 pos, Quaternion rot)
    {
        if (!PoolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool witht the tag: " + tag + " wasn't found in the pool dictionary.");
            return null;
        }
        if (PoolDictionary[tag].Count > 0)
        {        
            GameObject objToSpawn = PoolDictionary[tag].Dequeue();//take object from the pooldictionary and assign it as the object to spawn

            objToSpawn.SetActive(true);//set it to active
            objToSpawn.transform.position = pos;//set position
            objToSpawn.transform.rotation = rot;//set rotation

            IPooledObject iPooledObj = objToSpawn.GetComponent<IPooledObject>();
            if (iPooledObj != null)//if the object with the interface exist
            {
                iPooledObj.OnObjectSpawn();
            }
            return objToSpawn;
        }
        return null;
    }

    public void PutSpawnedObjectBackIntoPool(GameObject obj, string tag)
    {
        PoolDictionary[tag].Enqueue(obj);//put the object back into the pool to not lose it
    }
}
