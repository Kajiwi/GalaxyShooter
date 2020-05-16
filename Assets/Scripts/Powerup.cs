using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public float _speed = 5f;
    [SerializeField]
    private int powerupID;

    [SerializeField]
    private AudioClip _powerUpClip;

    // Update is called once per frame

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_powerUpClip, transform.position);

            if (player != null)
            {
                switch (powerupID)
                {
                    case 0: player.TurnTrippleShotActive();
                        break;

                    case 1: player.TurnSpeedPowerUpActive();
                        break;

                    case 2: player.TurnShieldPowerUpActive();
                        break;
                }

            }

            Destroy(this.gameObject);
        }
    }
}
