using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTail : MonoBehaviour
{
    [SerializeField] private GameObject _partToAdd;
    [SerializeField] private int _addingIndex = 4;
    [SerializeField] private float _distanceOfParts;
    [SerializeField] private List<Transform> _sankeTails = new List<Transform>();
    private List<Vector2> _pos = new List<Vector2>();
    private int _layerOdrer;
    private SpriteRenderer[] _tailEnd;
    private void Awake()
    {
        _tailEnd = new SpriteRenderer[3];
        for (int i = 0; i < 3; i++)
        {
            _tailEnd[i] = _sankeTails[_sankeTails.Count - 3 + i].GetComponent<SpriteRenderer>();
        }
    }
    private void Start()
    {
        for (int i = 0; i < _sankeTails.Count; i++)
        {
            _pos.Add(_sankeTails[i].position);
        }
    }

    private void Update()
    {
        float distance = ((Vector2)_sankeTails[0].position - _pos[0]).magnitude;

        if(distance > _distanceOfParts)
        {
            Vector2 dirction = ((Vector2)_sankeTails[0].position - _pos[0]).normalized;

            _pos.Insert(0, _pos[0] + dirction * _distanceOfParts);
            _pos.RemoveAt(_pos.Count - 1);

            distance -= _distanceOfParts;
        }

        for (int i = 1; i < _sankeTails.Count; i++)
        {
            _sankeTails[i].position = Vector2.Lerp(_pos[i], _pos[i - 1], distance / _distanceOfParts);

            Vector2 vector = _sankeTails[i - 1].position - _sankeTails[i].position;

            float angle = Vector2.Angle(vector, Vector2.right);

            if (vector.y < 0)
                angle = -angle;

            _sankeTails[i].rotation = Quaternion.Euler(0, 0, angle);
        }

    }

    public void AddTail()
    {
        Transform tail = Instantiate(_partToAdd,  _pos[_addingIndex], _sankeTails[_addingIndex].rotation, transform).transform;
        _sankeTails.Insert(_addingIndex, tail);
        _pos.Add(_pos[_pos.Count - 2]);

        _addingIndex++;
        tail.GetComponent<SpriteRenderer>().sortingOrder = _layerOdrer;
        LayerUpdate();
        _layerOdrer--;
    }

    private void LayerUpdate()
    {
        for (int i = 0; i < _tailEnd.Length; i++)
        {
            _tailEnd[i].sortingOrder = _layerOdrer - 1 - i;
        }
    }
}
