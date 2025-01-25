using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(TrunkRotator))]
public class Trunk : MonoBehaviour
{
    private readonly string _animationName = "TrunkHit";

    [SerializeField] private int _health = 5;
    [SerializeField] private Spawner _spawner;

    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _upwardsModifier;

    [SerializeField] private AudioSource _trunkBrakeSound;
    [SerializeField] private Animator _animator;

    [SerializeField] private Game _game;
    [SerializeField] private HighestLevelManager _highestLevelManager;

    private CircleCollider2D _collider;
    private TrunkRotator _rotator;
    private Rigidbody2D _rigidbody2D;

    private int _scoreValue = 1;

    public int Health => _health;
    
    public event UnityAction<int> HealthChanged;

    private void Start()
    {
        HealthChanged?.Invoke(_health);

        _collider = GetComponent<CircleCollider2D>();
        _rotator = GetComponent<TrunkRotator>();
        _rigidbody2D = GetComponent<Rigidbody2D>(); 
        _animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        _animator.Play(_animationName, -1, 0f);

        HealthChanged?.Invoke(_health);
        _game.AddScore(_scoreValue);

        if (Health <= 0) 
        {
            Explode();
        }
    }

    public void Explode()
    {
        _collider.enabled = false;

        Rigidbody[] partsRigidbodies = GetComponentsInChildren<Rigidbody>();
        Collider[] partColliders = GetComponentsInChildren<Collider>();

        foreach (Rigidbody part in partsRigidbodies) 
        {
            part.isKinematic = false;
            part.transform.parent = null;
            _rotator.StopRotate();
            part.AddExplosionForce(_explosionForce, transform.position, _explosionRadius, _upwardsModifier);
            part.AddForce(Vector3.down * 50f, ForceMode.VelocityChange);
            part.AddTorque(transform.position * 50f);

        }

        _trunkBrakeSound.Play();

        foreach (Collider item in partColliders)
            item.enabled = true;

        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;

        var knifes = GetComponentsInChildren<Knife>();

        foreach (Knife knife in knifes)
        {
            knife.DisableParent();
        }


        StartCoroutine(GoToNextLevel());

    }

    private IEnumerator GoToNextLevel()
    {
        yield return new WaitForSeconds(0.7f);
        _highestLevelManager.SaveHighLevel();
        Destroy(gameObject);

        int curentLevelIndex = SceneManager.GetActiveScene().buildIndex;

        int totalScenes = SceneManager.sceneCountInBuildSettings;

        if (curentLevelIndex + 1 < totalScenes)
            SceneManager.LoadScene(curentLevelIndex + 1);
        else
        {
            _rotator.StopRotate();
            _game.ShowFinishScreen();
        }

    }
}
    