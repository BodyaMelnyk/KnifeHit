using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkRotator : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    private void Update()
    {
        transform.Rotate(-Vector3.forward * _rotationSpeed * Time.deltaTime);
    }

    public void StopRotate()
    {
        _rotationSpeed = 0f;
    }
}
