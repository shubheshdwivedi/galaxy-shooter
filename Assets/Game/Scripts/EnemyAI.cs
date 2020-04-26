using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    private float _speed = 5.0f;

    [SerializeField] private GameObject explosionAnimation;
    
    private UIManager _uiManager;
    private GameManager _gameManager;
    [SerializeField] private AudioClip audioClip;
    
    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if(_gameManager.gameOver)
            Destroy(this.gameObject);
    }

    private void Movement()
    {
        transform.Translate(Time.deltaTime * _speed * Vector3.down);
        if(transform.position.y < -6.37f)
            transform.position = new Vector3(Random.Range(-7.8f, 7.8f),3.46f,0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Laser"))
        {
            if(other.transform.parent)
                Destroy(other.transform.parent.gameObject);
            Destroy(other.gameObject);
            Instantiate(explosionAnimation, transform.position,
                Quaternion.identity);
            _uiManager.UpdateScore();
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
            Destroy(this.gameObject);
        }

        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();
            if (player != null)
            {
                player.LoseLife();
                Instantiate(explosionAnimation, transform.position,
                    Quaternion.identity);
            }
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
            Destroy(this.gameObject);
        }
    }
}
