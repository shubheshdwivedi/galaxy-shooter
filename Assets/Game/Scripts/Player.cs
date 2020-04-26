using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public bool tripleShotEnabled = false;
    public bool shield = false;
    private float _speed = 5.0f;
    private int _lives = 3;
    private int _hitCount = 0;
    
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject tripleShotLaserPrefab;
    [SerializeField] private GameObject playerExplosion;
    [SerializeField] private GameObject shieldVisual;
    [SerializeField] private GameObject[] shipEngines;
    
    private const float FireRate = 0.25f;
    private float _nextFire = 0.0f;

    private UIManager _uiManager;
    private SpawnManager _spawnManager;
    private GameManager _gameManager;
    private AudioSource _audioSource;
    
    // Start is called before the first frame update
    void Start() 
    {
        transform.position = new Vector3(0, 0, 0);

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(_uiManager)
            _uiManager.UpdateLives(this._lives);

        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        if(_spawnManager)
            _spawnManager.StartCoroutines();

        _audioSource = GetComponent<AudioSource>();

        _hitCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if (!Input.GetKeyDown(KeyCode.Space) && (!Input.GetMouseButton(0) || !(Time.time > _nextFire))) return;
        Shoot();
    }
    
    // Player behaviour (native)
    private void Shoot()
    {
        _audioSource.Play();
        if (tripleShotEnabled)
            Instantiate(tripleShotLaserPrefab, transform.position, 
                Quaternion.identity);  
        
        else
            Instantiate(laserPrefab, transform.position + new Vector3(0, 0.88f, 0), 
                Quaternion.identity);    
        
        _nextFire = Time.time + FireRate;
    }
    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        var transformHelper = transform;
        transform.Translate(Time.deltaTime * _speed * horizontalInput * Vector3.right);
        transform.Translate(Time.deltaTime * _speed * verticalInput * Vector3.up);

        // Restriction on y axis
        if (transform.position.y > 0)
            transformHelper.position = new Vector3(transform.position.x, 0, 0);
        else if (transform.position.y < -4.2f)
            transformHelper.position = new Vector3(transform.position.x, -4.2f, 0);

        // Restriction on z axis + warping
        if (transformHelper.position.x > 9.5f)
            transformHelper.position = new Vector3(-9.5f, transformHelper.position.y, 0);
        else if (transformHelper.position.x < -9.5f)
            transformHelper.position = new Vector3(9.5f, transformHelper.position.y, 0);
    }

    
    // Player behaviour (affected)
    public void SpeedPowerUp()
    {
        this._speed *= 2.5f;
        StartCoroutine(SpeedPowerDown());
    }
    public void TripleShotPowerUp()
    {
        this.tripleShotEnabled = true;
        StartCoroutine(TripleShotPowerDown());
    }

    public void AddShield()
    {
        this.shield = true;
        shieldVisual.SetActive(true);
    }
    public void LoseLife()
    {
        if (this.shield)
        {
            this.shield = false;
            shieldVisual.SetActive(false);
            return;
        }
        this._lives--;
        this._hitCount++;
        _uiManager.UpdateLives(this._lives);
        if (this._hitCount == 1)
            shipEngines[1].SetActive(true);
        else if (this._hitCount == 2)
            shipEngines[0].SetActive(true);
        if (this._lives < 1)
        {
            _gameManager.gameOver = true;
            _uiManager.ShowTitleScreen();
            Instantiate(playerExplosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        
    }
    
    
    // Coroutines
    private IEnumerator TripleShotPowerDown()
    {
        yield return new WaitForSeconds(5.0f);
        this.tripleShotEnabled = false;
    }
    
    private IEnumerator SpeedPowerDown()
    {
        yield return new WaitForSeconds(5.0f);
        this._speed = 5.0f;
    }

    
}