using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;

public class BalloonMovement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _backGround;
 
    private Transform _balloonTransform;
    private SpriteRenderer _sprite;
    private CircleCollider2D _circleCollider;

    private float _rightEdge;
    private float _leftEdge;
    private float _balloonMovingCondition=0;

    private Vector3 _ballonScaleGrowth;

  



  
    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _circleCollider = GetComponent<CircleCollider2D>();
    
        _balloonTransform = GetComponent<Transform>();
        Bounds bounds=_backGround.bounds;
        _rightEdge = bounds.max.x;
        _leftEdge = bounds.min.x;

        _ballonScaleGrowth = _balloonTransform.localScale ;
        
       
    }

    void Update()
    {
      
        BallonMovement();
        IncreaseBalloonSize();

        
    }
    void BallonMovement()
    {
        if (_balloonMovingCondition == 0)
        {
            _balloonTransform.Translate(Vector3.right * 7* Time.deltaTime);
            if (_balloonTransform.position.x >= _rightEdge)
            {
                _balloonMovingCondition = 1;
            }

        }
        else if (_balloonMovingCondition == 1)
        {
            _balloonTransform.Translate(Vector3.left * 7 * Time.deltaTime);
            if (_balloonTransform.position.x <= _leftEdge)
            {
                _balloonMovingCondition = 0;
            }
        }

    }
    void IncreaseBalloonSize()
    {
        if ( _ballonScaleGrowth.x<= 5f) 
        {
            _ballonScaleGrowth += Vector3.one * Time.deltaTime*0.04f;
            _balloonTransform.localScale = _ballonScaleGrowth;
        }
        else
        {
            _balloonTransform.localScale = new Vector3(5, 5, 5);
            Destroy(gameObject);
            RestartLevel();
        }
    }
    private void RestartLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex); 
    }



}
