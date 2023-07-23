using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector2 _direction;
    private Rigidbody2D _rb2d;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        _rb2d.velocity = _direction * _speed;
    }
}
