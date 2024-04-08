using System.Collections.Generic;
using UnityEngine;

public class Array_Pool : MonoBehaviour
{
    public static Array_Pool Instance;

    [SerializeField]
    private GameObject poolingObjectPrefab;

    private Bullet[] poolingObjectArray;

    private int currentIndex = 0;

    private void Awake()
    {
        Instance = this;

        Initialize(10);
    }

    private void Initialize(int initCount)
    {
        poolingObjectArray = new Bullet[initCount];

        for (int i = 0; i < initCount; i++)
        {
            poolingObjectArray[i] = CreateNewObject();
        }
    }

    private Bullet CreateNewObject()
    {
        var newObj = Instantiate(poolingObjectPrefab).GetComponent<Bullet>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        newObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        return newObj;
    }

    public static Bullet GetObject()
    {
        if (Instance.currentIndex < Instance.poolingObjectArray.Length)
        {
            var obj = Instance.poolingObjectArray[Instance.currentIndex];
            Instance.currentIndex++;
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewObject();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);
            newObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
            return newObj;
        }
    }

    public static void ReturnObject(Bullet obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.currentIndex--;
    }
}