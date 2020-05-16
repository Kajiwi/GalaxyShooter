using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4;

    private Player _player;

    [SerializeField]
    private Animator _enemyAnimator;

    [SerializeField]
    private AudioClip _Explosionclip;
    private AudioSource _audiosource;

    [SerializeField]
    private GameObject _laserPref;

    private float _fireRate = 3f;
    private float _canFire = -1f;


    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _enemyAnimator = GetComponent<Animator>();
        _audiosource = GetComponent<AudioSource>();

        if (_enemyAnimator == null)
        {
            Debug.LogError("Enemy animator is null");
        }

        if (_audiosource == null)
        {
            Debug.LogError("Audiosource is null");
        }
        else
        {
            _audiosource.clip = _Explosionclip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;

            GameObject _enemLaser = Instantiate(_laserPref, transform.position, Quaternion.identity);
            Laser[] laser = _enemLaser.GetComponentsInChildren<Laser>();
            for (int i = 0; i < laser.Length; i++)
            {
                laser[i].TurnEnemyLaserOn();
            }
        }
    }

    private void CalculateMovement()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        if (transform.position.y <= -4f)
        {
            float randomX = Random.Range(8.5f, -8.5f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

            _audiosource.Play();
            _enemyAnimator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject, 2.8f);

        }

        else if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if(_player != null)
            {
                _player.IncreaseScore(Random.Range(10,12));
            }

            _audiosource.Play();
            _enemyAnimator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.8f);

        }
    }
}
