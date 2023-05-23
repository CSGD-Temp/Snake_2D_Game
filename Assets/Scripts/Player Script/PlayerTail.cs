using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTail : MonoBehaviour
{
    [SerializeField] private GameObject _partToAdd;
    [SerializeField] private Transform _headPos;
    [SerializeField] private int _addingIndex;
    [SerializeField] private float _distanceOfParts;
    [SerializeField] private List<Transform> _sankeTails = new List<Transform>();
    private List<Vector2> _pos = new List<Vector2>();

    private void Start()
    {
        _pos.Add(_headPos.position);

        for (int i = 0; i < _sankeTails.Count; i++)
        {
            _pos.Add(_sankeTails[i].position);
        }
    }

    private void Update()
    {
        float distance = ((Vector2)_headPos.position - _pos[0]).magnitude;

        if(distance > _distanceOfParts)
        {
            Vector2 dirction = ((Vector2)_headPos.position - _pos[0]).normalized;

            _pos.Insert(0, _pos[0] + dirction * _distanceOfParts);
            _pos.RemoveAt(_pos.Count - 1);

            distance -= _distanceOfParts;
        }

        for (int i = 0; i < _sankeTails.Count; i++)
        {
            _sankeTails[i].position = Vector2.Lerp(_pos[i + 1], _pos[i], distance / _distanceOfParts);

            Vector2 vector = _pos[i] - _pos[i + 1];

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
    }
}
