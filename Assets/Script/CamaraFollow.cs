using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFollow : MonoBehaviour
{
    private GameObject _player;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerScript>().gameObject;
    }

    private void Update()
    {
        transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, -10);
    }
}
