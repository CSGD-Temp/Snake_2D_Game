using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _maxDisFood;
    [SerializeField] private float _TongueAnimationDelay;
    [SerializeField] private string _fruitTag;
    [SerializeField] private string _obstaclesTag;
    [SerializeField] private Transform _playerHead;
    [SerializeField] private GameObject _leye, _reye;
    [SerializeField] private GameObject _deadAnimation;
    [SerializeField] private GameObject _TongueAnimation;
    [SerializeField] private LayerMask _layerMask;

    [Header("Scripts")]
    [SerializeField] private friutspwner _friutspwner;

    private Rigidbody2D _rbody;
    private Vector2 _mousePos;
    private float angle;
    private Quaternion quaternion;
    private PlayerTail playerTail;
    private bool _isPlayerDead;
    private void Awake()
    {
        _rbody = GetComponent<Rigidbody2D>();
        playerTail = GetComponent<PlayerTail>();
    }
    private void Start()
    {
        StartCoroutine(PlayTongueAnimation());
    }
    private void Update()
    {
        Movement();
        EyeAnimate();
    }

    private void FixedUpdate()
    {
        if (!_isPlayerDead)
        {
            _rbody.velocity = quaternion * Vector2.right * _moveSpeed * Time.fixedDeltaTime;

            _rbody.MoveRotation(quaternion.eulerAngles.z);
        }
        else
        {
            _rbody.velocity = Vector2.zero;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == _fruitTag)
        {
            Destroy(collision.gameObject);//destroy friut
            _friutspwner.friutSpawner();//spawn new friut
            playerTail.AddTail();
        }
        else if(collision.gameObject.tag == _obstaclesTag)
        {
            _isPlayerDead = true;
            _deadAnimation.SetActive(true);
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

    private IEnumerator PlayTongueAnimation()
    {
        float delay1 = Random.Range(_TongueAnimationDelay, _TongueAnimationDelay + 3);
        yield return new WaitForSeconds(delay1);

        _TongueAnimation.SetActive(true);

        yield return new WaitForSeconds(1);

        _TongueAnimation.SetActive(false);

        StartCoroutine(PlayTongueAnimation());
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(_playerHead.position, _maxDisFood);
    }

}
