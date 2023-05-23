using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private string _fruitTag;

    private Rigidbody2D _rbody;
    private Vector2 _mousePos;
    private float angle;
    private Quaternion quaternion;
    private PlayerTail playerTail;

    private void Awake()
    {
        _rbody = GetComponent<Rigidbody2D>();
        playerTail = GetComponent<PlayerTail>();
    }
    private void Update()
    {
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 vector = _mousePos - (Vector2)transform.position;

        angle = Vector2.Angle(vector, Vector2.right);

        if (vector.y < 0)
            angle = -angle;

        quaternion = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), _rotateSpeed * Time.deltaTime);

        // for test
        /*if (Input.GetMouseButtonDown(0))
        {
            GetComponent<PlayerTail>().AddTail();
        }*/
    }

    private void FixedUpdate()
    {
        _rbody.velocity = quaternion * Vector2.right * _moveSpeed * Time.fixedDeltaTime;

        _rbody.MoveRotation(quaternion.eulerAngles.z);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == _fruitTag)
        {
            Destroy(collision.gameObject);
            playerTail.AddTail();
        }
    }
}
