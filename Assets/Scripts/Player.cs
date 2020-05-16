using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _speedMultiplier = 2f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private GameObject _trippleLaserPref;
    [SerializeField]
    private GameObject _shieldpref;

    [SerializeField]
    private bool isTrippleShotActive = false;
    [SerializeField]
    private bool isSpeedPowerUpActive = false;
    [SerializeField]
    private bool isShieldPowerUpActive = false;

    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private GameObject _leftEngine;

    [SerializeField]
    private int _score;

    private UIManager _uiManager;

    [SerializeField]
    private AudioClip _laserSoundClip;
    [SerializeField]
    private AudioClip _explosionSound;
    private AudioSource _audioSource;


    // Start is called before the first frame update
    void Start()
    {
        _rightEngine.SetActive(false);
        _leftEngine.SetActive(false);

        this.transform.position = Vector3.zero;

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();

        if (_spawnManager == null)
        {
            Debug.LogError("spawnmanager is null");
        }

        if (_uiManager == null)
        {
            Debug.LogError("UI Manager is null");
        }

        if (_audioSource == null)
        {
            Debug.LogError("Audisource is null");
        }

    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }

    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
 
        transform.Translate(direction * Time.deltaTime * _speed);
        
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }

        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    private void FireLaser()
    {
        _canFire = Time.time + _fireRate;

        if (isTrippleShotActive == true)
        {
            Vector3 offsetPos = transform.position + new Vector3(0, 1.90f, 0);
            Instantiate(_trippleLaserPref, offsetPos, Quaternion.identity);
        }
        else
        {
            Vector3 offsetPos = transform.position + new Vector3(0, 1.05f, 0);
            Instantiate(_laserPrefab, offsetPos, Quaternion.identity);
        }

        _audioSource.clip = _laserSoundClip;
        _audioSource.Play();
    }

    public void Damage ()
    {
        if (isShieldPowerUpActive == true)
        {
            isShieldPowerUpActive = false;
            _shieldpref.SetActive(false);
            return;
        }

        _lives -= 1;

        if (_lives == 2)
        {
            _rightEngine.SetActive(true);
        }

        else if (_lives == 1)
        {
            _leftEngine.SetActive(true);
        }

        _uiManager.UpdateLives(_lives);

        if (_lives < 1)
        {

            if (_spawnManager != null)
            {
               _spawnManager.OnPlayerDeath();
            }

            Destroy(_leftEngine.gameObject);
            Destroy(_rightEngine.gameObject);
            Destroy(this.gameObject);
        }
    }

    public void TurnShieldPowerUpActive ()
    {
        isShieldPowerUpActive = true;
        _shieldpref.SetActive(true);
    }

    public void TurnTrippleShotActive()
    {
        isTrippleShotActive = true;
        StartCoroutine(TurnOffPowerUp());
    }

    IEnumerator TurnOffPowerUp()
    {
        yield return new WaitForSeconds(5f);
        isTrippleShotActive = false;
    }

    public void TurnSpeedPowerUpActive ()
    {
        isSpeedPowerUpActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(TurnSpeedOff());
    }

    IEnumerator TurnSpeedOff ()
    {
        yield return new WaitForSeconds(5f);
        isSpeedPowerUpActive = false;
        _speed /= _speedMultiplier;
    }

    public void IncreaseScore(int points)
    {
        _score += points;
        _uiManager.PlayerScoreUpdate(_score);
        //communicate UI to update score
    }

}
