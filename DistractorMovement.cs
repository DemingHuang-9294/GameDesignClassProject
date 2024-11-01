using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DistractorMovement : MonoBehaviour
{

    public float _speed = 10f;
    private Vector3 _direction;

     private Transform _Ob;
    private Vector2 _maxPosition;
    private Vector2 _minPosition;


    void Awake()
        {
      
        _direction = Vector3.right * (Random.value > 0.5f ? 1 : -1);
        _Ob =GetComponent<Transform>();
        _maxPosition = new Vector2(_Ob.position.x + 10f, _Ob.position.y);
        _minPosition = new Vector2(_Ob.position.x -10f, _Ob.position.y);
    }
    
    void Update()
    {
        _Ob.Translate(_direction * _speed * Time.deltaTime);

        
        if(_Ob.position.x >= _maxPosition.x || _Ob.position.x <= _minPosition.x )
        {
            Destroy(gameObject);
        }


    }
}
