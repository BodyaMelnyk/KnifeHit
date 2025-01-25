using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Knife : MonoBehaviour
{
    [SerializeField] private int _damage = 1;
    [SerializeField] private AudioSource _knifeHit;
    [SerializeField] private AudioSource _knifeHitFall;
    [SerializeField] private float _reflectionForce;

    private Rigidbody2D _rigidbody2D;
    private Trunk _trunk;
    private Spawner _spawner;
    private BoxCollider2D _collider;
    private TrunkRotator _trunkRotator;
    private AdsPanel _adsPanel;

    public void Init(Trunk trunk, TrunkRotator trunkRotator, AdsPanel adsPanel)
    {
        _trunk = trunk;
        _trunkRotator = trunkRotator;
        _adsPanel = adsPanel;
    }

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spawner = FindObjectOfType<Spawner>();
        _collider = GetComponent<BoxCollider2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out KnifeCollision another))
        {
            _trunkRotator.StopRotate();
            _spawner.StopSpawnKnife();
            _knifeHitFall.Play();
            _rigidbody2D.AddForce(Vector2.down * _reflectionForce, ForceMode2D.Impulse);

            _adsPanel.ShowPanel();

            return;
        }

        else if (collision.TryGetComponent(out Trunk trunk))
        {
            trunk.TakeDamage(_damage);
            _rigidbody2D.velocity = Vector2.zero;
            transform.parent = collision.transform;

            _spawner.SpawnKnife();

            _collider.isTrigger = false;

            _knifeHit.Play();
        }

    }

    public void DisableParent()
    {
        float randomXForce = Random.Range(-20f, 20f);
        float yForce = 1f;

        gameObject.transform.parent = null;
        _rigidbody2D.gravityScale = 1;
        _rigidbody2D.AddForce(new Vector2(randomXForce, yForce) * 5f, ForceMode2D.Impulse);
        _rigidbody2D.AddTorque(5f, ForceMode2D.Impulse);
    }

}
