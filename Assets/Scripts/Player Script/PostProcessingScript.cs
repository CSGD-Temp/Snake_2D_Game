using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingScript : MonoBehaviour
{
    [SerializeField] private float _maxBloom;
    [SerializeField] private float _delay;

    private Bloom _bloom;
    private float _bloomValue;
    private GameObject _player;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerScript>().gameObject;
        GetComponent<PostProcessVolume>().profile.TryGetSettings<Bloom>(out _bloom);
    }

    private IEnumerator BloomIncrese()
    {
        yield return new WaitForSeconds(_delay);

        _bloomValue += .25f;
        _bloom.intensity.value = _bloomValue;

        if (_bloomValue < _maxBloom)
        {
            StartCoroutine(BloomIncrese());
        }
        else
        {
            _player.SetActive(false);
            StartCoroutine(BloomDecrese());
        }
    }

    private IEnumerator BloomDecrese()
    {
        yield return new WaitForSeconds(_delay);

        _bloomValue -= .25f;
        _bloom.intensity.value = _bloomValue;

        if (_bloomValue > 0)
        {
            StartCoroutine(BloomDecrese());
        }
    }

    public void UpdateBloom()
    {
        StartCoroutine(BloomIncrese());
    }
}


