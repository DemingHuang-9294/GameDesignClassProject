using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject _pinPrefab;
    [SerializeField] private Transform _shootPosition;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Transform _BalloonSize;





    private Rigidbody2D _playerRigidbody;
    private Transform _playerTransform;
    private float _horizontailInput;
    private int _jumpedCount=0;
    private float _score=0;
    private float _scoreSize;
    private float _timeInterval;
    private float _count = 0;

     private Pin _pin;
    private void Awake()
    {
        _playerTransform = GetComponent<Transform>();
        _playerRigidbody= GetComponent<Rigidbody2D>();
        _scoreText.text = "score" + 0;
        _timeInterval = 1.5f;

    }


    void Update()
    {
        _timeInterval += Time.deltaTime;
        
        _horizontailInput = Input.GetAxis("Horizontal");


        //Moving of Player
        Moving();
        if (Input.GetKeyDown(KeyCode.Space) && _jumpedCount<3)
        {
            
            _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, 10);
            _jumpedCount++;
        }
        if (Input.GetButtonDown("Fire1")&& _timeInterval>=1.5f)
        {
            ShootPin();
            _timeInterval = 0f;
            _count++;
        }
        if (_count >= 5)
        {
            RestartLevel();
        }

        _scoreText.text = "Score: " + _score;
        if (_score >= 5)
        {
            LoadNextLevel();
        }
    }
    void Moving()
    {
        if (_horizontailInput != 0)
        {
            _playerRigidbody.velocity = new Vector2(_horizontailInput*5f, _playerRigidbody.velocity.y);
            if (_horizontailInput > 0.01f)
            {
                transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            }
            else if(_horizontailInput<-0.01f)
            { 
                transform.localScale = new Vector3(-0.75f, 0.75f, 0.75f);
            }
        }
    }

    void ShootPin()
    {
        GameObject newPin = Instantiate(_pinPrefab, _shootPosition.position  ,Quaternion.identity);
        // Pin _pin=newPin.GetComponent<Pin>();
         _pin = newPin.GetComponent<Pin>();      

        _pin.PinDirection(Mathf.Sign(transform.localScale.x));
        
    }

    public void minusScore()
    {
        if (_score != 0)
        {
            _score--;
        }
    }
    public void addScore()
    {
        if (_BalloonSize.localScale.x<=3.5f)
        {
            _score += 3;
        }else if (_BalloonSize.localScale.x <= 4.2f)
        {
            _score += 2;
        }
        else
        {
            _score += 1;
        }
        

    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex); 
        }
        else
        {
            SceneManager.LoadScene(0); 
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.CompareTag("Ground")) 
        {
            
            _jumpedCount = 0;
        }
        
    }
    private void RestartLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

}
