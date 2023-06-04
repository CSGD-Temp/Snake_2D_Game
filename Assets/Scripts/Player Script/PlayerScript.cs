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
    public int _maxFoodCanEat;
    [SerializeField] private string _fruitTag;
    [SerializeField] private string _obstaclesTag;
    [SerializeField] private Transform _playerHead;
    [SerializeField] private GameObject _leye, _reye;
    [SerializeField] private GameObject _deadAnimation;
    [SerializeField] private GameObject _TongueAnimation;
    [SerializeField] private LayerMask _layerMask;
    private AudioSource _audioSource; //audio source for friut
    [SerializeField] private AudioClip _audioClip0;
    [SerializeField] private AudioClip _audioClip1;
    [SerializeField] private AudioClip _audioClip2;


    [Header("Scripts")]
    [SerializeField] private friutspwner _friutspwner;

    private Rigidbody2D _rbody;
    private Vector2 _mousePos;
    private float angle;
    public int eatFoodCount;
    private Quaternion quaternion;
    private PlayerTail playerTail;
    private PostProcessingScript processingScript;

    public bool isPlayerDead;
    public bool isPlayerWin;

    private void Awake()
    {
        _rbody = GetComponent<Rigidbody2D>();
        playerTail = GetComponent<PlayerTail>();
        processingScript = FindObjectOfType<PostProcessingScript>();
        _audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        StartCoroutine(PlayTongueAnimation());
        GameObject friut = GameObject.FindGameObjectWithTag("Fruit");//get friut object
        //_audioSource = friut.AddComponent<AudioSource>();//get audio source from friut
    }
    private void Update()
    {
        Movement();
        EyeAnimate();
    }

    private void FixedUpdate()
    {
        if (isPlayerDead || isPlayerWin)
        {
            _rbody.velocity = Vector2.zero;
        }
        else
        {
            _rbody.velocity = quaternion * Vector2.right * _moveSpeed * Time.fixedDeltaTime;

            _rbody.MoveRotation(quaternion.eulerAngles.z);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == _fruitTag)
        {
            Destroy(collision.gameObject);//destroy friut
            _audioSource.clip = _audioClip0;
            _audioSource.Play();//play audio
            PlayerWin();
            if (!isPlayerWin)
                _friutspwner.friutSpawner();//spawn new friut
            playerTail.AddTail();
        }
        else if (collision.gameObject.tag == _obstaclesTag)
        {
            _audioSource.clip = _audioClip2;
            _audioSource.Play();//play audio
            isPlayerDead = true;
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

        if (collider != null)
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

        if (!isPlayerDead)
        {
            _audioSource.clip = _audioClip1;
            _audioSource.Play();
            _TongueAnimation.SetActive(true);
        }

        yield return new WaitForSeconds(1);

        _TongueAnimation.SetActive(false);

        StartCoroutine(PlayTongueAnimation());
    }

    private void PlayerWin()
    {
        eatFoodCount++;
        if (eatFoodCount >= _maxFoodCanEat && !isPlayerWin)
        {
            processingScript.UpdateBloom();
            isPlayerWin = true;
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(_playerHead.position, _maxDisFood);
    }

}
