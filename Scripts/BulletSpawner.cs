using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{

    //public GameObject bulletPrefab;

    public float spawnRateMin = 0.1f;
    public float spawnRateMax = 1f;

    private Transform _target;
    private float _spawnRate;
    private float _timeAfterSpawn;
    // Start is called before the first frame update
    void Awake()
    {
        _spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        _target = FindObjectOfType<PlayerController>().transform;
    }

    void Start()
    {
        _timeAfterSpawn = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        _timeAfterSpawn += Time.deltaTime;


        if (_timeAfterSpawn >= _spawnRate)
        {
            _timeAfterSpawn = 0;

            var bulletGameObject = ObjectPoolManager.Instance.GetObject();
            if (bulletGameObject != null)
            {
                bulletGameObject.transform.position = transform.position;

                bulletGameObject.transform.LookAt(_target);
                bulletGameObject.gameObject.SetActive(true);
                //ObjectPoolManager.Instance.ReturnObject(bullet.GetComponent<Bullet>());
            }
            else
            {
                Debug.Log("비었음!");
            }

            _spawnRate = Random.Range(spawnRateMin, spawnRateMax);

        }

    }
}
