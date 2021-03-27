using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
    Dictionary<int, List<GameObject>> poolDictionary = new Dictionary<int, List<GameObject>>();

    static GameObjectPool _instance;
    public GameObject[] gameObjects;

    public static GameObjectPool instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameObjectPool>();
            }
            return _instance;
        }
    }

    void Start()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            CreatePool(gameObjects[i], 20);
        }
    }

    public void CreatePool(GameObject prefab, int poolSize)
    {
        int poolKey = prefab.GetInstanceID();

        if (!poolDictionary.ContainsKey(poolKey))
        {
            poolDictionary.Add(poolKey, new List<GameObject>());

            for (int i = 0; i < poolSize; i++)
            {
                GameObject newObject = Instantiate(prefab);
                poolDictionary[poolKey].Add(newObject);
                newObject.SetActive(false);
            }
        } else
        {
            for (int i = 0; i < poolSize; i++)
            {
                GameObject newObject = Instantiate(prefab);
                poolDictionary[poolKey].Add(newObject);
                newObject.SetActive(false);
            }
        }
    }

    public GameObject GetObject(GameObject prefab)
    {
        int poolKey = prefab.GetInstanceID();

        if (poolDictionary.ContainsKey(poolKey))
        {
            List<GameObject> pool = poolDictionary[poolKey];

            for (int i = 0; i < pool.Count; i++)
            {
                if (!pool[i].gameObject.activeInHierarchy)
                {
                    return pool[i];
                }
            }
            GameObject newObject = Instantiate(prefab);
            poolDictionary[poolKey].Add(newObject);
            return newObject;
        }
        return null;
    }
}