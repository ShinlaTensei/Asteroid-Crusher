using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    #region Singleton
    private static ObjectPooler m_instance;
    public static ObjectPooler Instance { get { return m_instance; } }
    #endregion
    [System.Serializable]
    public class PoolObject
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    public List<PoolObject> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        m_instance = this;
    }
    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (PoolObject poolObject in pools)
        {
            int amount = poolObject.size;
            Queue<GameObject> queueObject = new Queue<GameObject>();
            for (int i = 0; i < amount; i++)
            {
                GameObject obj = Instantiate(poolObject.prefab);
                obj.SetActive(false);
                queueObject.Enqueue(obj);
            }

            poolDictionary.Add(poolObject.tag, queueObject);
        }
        GameManager.Instance.MainGameStart();
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (poolDictionary.ContainsKey(tag) == false)
        {
            Debug.LogWarning(tag + " is not defined.");
            return null;
        }
        GameObject poolObject = poolDictionary[tag].Dequeue();
        poolObject.transform.SetPositionAndRotation(position, rotation);
        poolObject.SetActive(true);
        poolDictionary[tag].Enqueue(poolObject);
        return poolObject;
    }

}
