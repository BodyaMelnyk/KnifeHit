using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class KnifeThrower : MonoBehaviour
{
    [SerializeField] private float _throwForce;

    private bool _isThrown = false;

    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!_isThrown && Input.GetMouseButtonDown(0))
        {
            Throw();
        } 
    }


    private void Throw()
    {
        _rigidbody2D.AddForce(Vector3.up * _throwForce, ForceMode2D.Impulse);
        _isThrown = true;
    }


}
