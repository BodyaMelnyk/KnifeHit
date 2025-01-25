using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Knife _prefab;
    [SerializeField] private Trunk _target;
    [SerializeField] private TrunkRotator _trunkRotator;
    [SerializeField] private AdsPanel _adsPanel;

    private Knife _spawned;
    private bool _canSpawn = true;

    private void Awake()
    {
        SpawnKnife();
    }

    public void SpawnKnife()
    {
        if(_canSpawn == true) 
        {
            if(_target.Health > 0)
            {
                _spawned = Instantiate(_prefab, transform.position, Quaternion.identity).GetComponent<Knife>();
                _spawned.Init(_target, _trunkRotator, _adsPanel);
            }

            return;
        }
            
    }

    public void StopSpawnKnife()
    {
        _canSpawn = false;
    }


}
