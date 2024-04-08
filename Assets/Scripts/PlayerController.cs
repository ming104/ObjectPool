using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _playerRigidbody;
    public float speed = 8f;


    // Start is called before the first frame update
    void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        float xSpeed = xInput * speed;
        float zSpeed = zInput * speed;

        Vector3 newVelocity = new Vector3(xSpeed, 0f, zSpeed);

        _playerRigidbody.velocity = newVelocity;
    }

    public void Die()
    {
        //겜 오브젝트 비활성화
        gameObject.SetActive(false);

        GameManager gameManager = FindObjectOfType<GameManager>();

        gameManager.EndGame();
    }
}
