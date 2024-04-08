// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Pool;

// public class Bullet : PoolAble
// {
//     //public IObjectPool<GameObject> Pool { get; set; }

//     public float speed = 8f;
//     private Rigidbody _bulletRigidbody;

//     // Start is called before the first frame update
//     void Start()
//     {
//         _bulletRigidbody = GetComponent<Rigidbody>();
//         _bulletRigidbody.velocity = transform.forward * speed;

//         //Destroy(gameObject, 3f);
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if (this.transform.position.y > 5)
//         {
//             ReleaseObject();
//         }
//     }

//     // private void OnTriggerEnter(Collider other)
//     // {
//     //     if (other.CompareTag("Player"))
//     //     {
//     //         PlayerController playerController = other.GetComponent<PlayerController>();

//     //         if (playerController != null)
//     //         {
//     //             playerController.Die();
//     //         }


//     //     }
//     // }
// }
