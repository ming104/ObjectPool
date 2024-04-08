//#define _IObjectPooling
//#define _List_Pooling
#define _Array_Pooling


using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{
    private static ObjectPoolManager instance = null;
    void Awake()
    {
        if (null == instance)
        {
            instance = this;

            //DontDestroyOnLoad(this.gameObject);

        }
        Init();
    }
    public static ObjectPoolManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    public GameObject bulletPrefab;

    public int maxPoolSize = 15;

#if _IObjectPooling

    public IObjectPool<GameObject> Pool { get; private set; }
    public int defaultCapacity = 10;

    public void Init()
    {
        Pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,
        OnDestroyPoolObject, true, defaultCapacity, maxPoolSize);

        for (int i = 0; i < defaultCapacity; i++)
        {
            Bullet bullet = CreatePooledItem().GetComponent<Bullet>();
            bullet.Pool.Release(bullet.gameObject);
        }
    }

    private GameObject CreatePooledItem()
    {
        GameObject poolGo = Instantiate(bulletPrefab);
        poolGo.GetComponent<Bullet>().Pool = this.Pool;
        return poolGo;
    }

    private void OnTakeFromPool(GameObject poolGo)
    {
        poolGo.SetActive(true);
    }

    private void OnReturnedToPool(GameObject poolGo)
    {
        poolGo.SetActive(false);
    }

    private void OnDestroyPoolObject(GameObject poolGo)
    {
        Destroy(poolGo);
    }

    public GameObject GetObject()
    {
        //Debug.Log("1");
        var bullet = Pool.Get();
        bullet.SetActive(false);
        return bullet;
    }
    public void ReturnObject(GameObject obj)
    {
        Pool.Release(obj);
    }

#elif _List_Pooling

    List<GameObject> poolingObjectList = new List<GameObject>();

    public void Init()
    {
        for (int i = 0; i < maxPoolSize; i++)
        {
            poolingObjectList.Add(CreateNewObject());
        }
        Debug.Log("실행됨");
    }

    private GameObject CreateNewObject()
    {
        var newObj = Instantiate(bulletPrefab);
        newObj.SetActive(false);
        return newObj;
    }

    public GameObject GetObject()
    {
        if (Instance.poolingObjectList.Count > 0)
        {
            var obj = Instance.poolingObjectList[0];
            Instance.poolingObjectList.RemoveAt(0);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewObject();
            //newObj.SetActive(false);
            return newObj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        Instance.poolingObjectList.Add(obj);
    }

#elif _Array_Pooling

    private GameObject[] poolingObjectArray;
    private int currentIndex = 0;
    public void Init()
    {
        poolingObjectArray = new GameObject[maxPoolSize];
        for (int i = 0; i < maxPoolSize; i++)
        {
            poolingObjectArray[i] = CreateNewObject();
        }
    }

    private GameObject CreateNewObject()
    {
        var newObj = Instantiate(bulletPrefab);
        newObj.SetActive(false);
        return newObj;
    }

    public GameObject GetObject()
    {
        if (currentIndex < maxPoolSize)
        {
            Debug.Log("얘는 있음" + currentIndex + maxPoolSize);
            GameObject obj = poolingObjectArray[currentIndex];
            Debug.Log(obj);
            currentIndex++;
            return obj;
        }
        else
        {
            Debug.Log("없으니까 소환함");
            var newObj = CreateNewObject();
            newObj.SetActive(false);
            return newObj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = 0;
        }
        poolingObjectArray[currentIndex] = obj;
    }


#endif

}