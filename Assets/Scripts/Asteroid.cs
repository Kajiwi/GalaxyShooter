using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private GameObject _explosion;
    private SpawnManager _spawnMan;

    [SerializeField]
    private AudioClip _explosionClip;
    private AudioSource _audiosource;

    private void Start()
    {
        _spawnMan = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _audiosource = GetComponent<AudioSource>();

        if (_audiosource == null)
        {
            Debug.LogError("Audiosource is null");
        }
        else
        {
            _audiosource.clip = _explosionClip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotationAst = new Vector3(0, 0, 3);
        transform.Rotate(rotationAst * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Instantiate(_explosion, this.transform.position, Quaternion.identity);
            _audiosource.Play();
            Destroy(other.gameObject);
            _spawnMan.StartSpawning();
            Destroy(this.gameObject, 1f);
        }
    }
}
