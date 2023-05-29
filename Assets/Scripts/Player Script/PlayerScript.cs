using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private string _fruitTag;
    [SerializeField] private Transform _playerHead;
    [SerializeField] private float _maxDisFood;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private GameObject _leye, _reye;

    [Header("Scripts")]
    [SerializeField] private friutspwner _friutspwner;

    private Rigidbody2D _rbody;
    private Vector2 _mousePos;
    private float angle;
    private Quaternion quaternion;
    private PlayerTail playerTail;
    private bool _nearFood;
    private void Awake()
    {
        _rbody = GetComponent<Rigidbody2D>();
        playerTail = GetComponent<PlayerTail>();
    }
    private void Update()
    {
        Movement();
        EyeAnimate();
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
            Destroy(collision.gameObject);//destroy friut
            _friutspwner.friutSpawner();//spawn new friut
            playerTail.AddTail();
        }
    }

    private void Movement()
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

    private void EyeAnimate()
    {
        Collider2D collider = Physics2D.OverlapCircle(_playerHead.position, _maxDisFood, _layerMask);

        if(collider != null)
        {
            Vector2 vector = collider.transform.position - _playerHead.position; 

            float angle1 = Vector2.Angle(vector, Vector2.right);

            if (vector.y < 0)
                angle1 = -angle1;

            _leye.transform.rotation = Quaternion.Euler(0, 0, angle1);
            _reye.transform.rotation = Quaternion.Euler(0, 0, angle1);

        }
        else
        {
            _leye.transform.rotation = Quaternion.Euler(0, 0, _playerHead.eulerAngles.z);
            _reye.transform.rotation = Quaternion.Euler(0, 0, _playerHead.eulerAngles.z);
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(_playerHead.position, _maxDisFood);
        Gizmos.color = Color.red;
    }

}
