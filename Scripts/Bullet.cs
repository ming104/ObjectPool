using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class Bullet : PoolAble
{
    //public IObjectPool<GameObject> Pool { get; set; }
    //private Transform _target;
    public float speed = 8f;
    private Rigidbody _bulletRigidbody;

    // Start is called before the first frame update

    void OnEnable()
    {
        _bulletRigidbody = GetComponent<Rigidbody>();
        _bulletRigidbody.velocity = transform.forward * speed;
        StartCoroutine(Return_Obj());
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();

            if (playerController != null)
            {
                playerController.Die();
            }


        }
    }

    private IEnumerator Return_Obj()
    {
        yield return new WaitForSeconds(3f);
        ObjectPoolManager.Instance.ReturnObject(gameObject);
    }
}

