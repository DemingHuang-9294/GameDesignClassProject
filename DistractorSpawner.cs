using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractorSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    private float _timer=0f;
    private float _spawnTime=1f;




    [SerializeField] private SpriteRenderer _edge;
    private float _upEdge;
    private float _downEdge;
    private float _rightEdge;
    private float _leftEdge;
    void Awake()
    {
        Bounds bounds = _edge.bounds;
        _upEdge = bounds.max.y;
        _downEdge = bounds.min.y;
        _rightEdge=bounds.max.x;
        _leftEdge= bounds.min.x;
}

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer>=_spawnTime)
        {
            SpawnObstacle();
            _timer = 0f;
        }
    }
    void SpawnObstacle()
    {
        float yPos =Random.Range( _downEdge,_upEdge);
        float xPos= Random.Range( _leftEdge,_rightEdge);
        Vector3 spawnPosition = new Vector3(xPos, yPos, 0);
        Instantiate(_prefab, spawnPosition, Quaternion.identity);
    }
}
