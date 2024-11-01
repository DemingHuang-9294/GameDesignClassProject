using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    private Transform _pinTransform;
    private float _direction = 0;
    private AudioSource _ballonPop;
  

    private void Awake()
    {
        _pinTransform = GetComponent<Transform>();
        _ballonPop = GetComponent<AudioSource>();
    }
    void Update()
    {
        PinMove();
        
    }
    void PinMove()
    {
        if (_direction >=0.01f)
        {
            _pinTransform.Translate(Vector3.right * 10f * Time.deltaTime);
            _pinTransform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        }
        else if (_direction <=-0.01f)
        {
            _pinTransform.Translate(Vector3.left * 10f * Time.deltaTime);
            _pinTransform.localScale = new Vector3(-0.6f, 0.6f, 0.6f);
        }

    }

    public void PinDirection(float direction)
    {
        _direction = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Balloon"))
        {
            Debug.Log("Balloon hit!");
            _ballonPop.Play();
           
            FindObjectOfType<PlayerMovement>().addScore();
            Destroy(collision.gameObject);
            Destroy(gameObject,_ballonPop.clip.length);
           
            
            
        }
        if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Ob"))
        {
            FindObjectOfType<PlayerMovement>().minusScore();
            Destroy(gameObject);
        }
    }
   
  

    
}
