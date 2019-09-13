using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 0.8f;
    [SerializeField]
    private int _powerupID;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Colided with: " + other.name);
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                if (_powerupID == 0)
                {
                    player.TripleShotPowerupOn();
                }
                else if (_powerupID == 1)
                {
                    player.IncreaseSpeedOn();
                }
                else if (_powerupID == 2)
                {
                    player.ShieldOn();
                }
            }

            Destroy(this.gameObject);
        }
    }
}
