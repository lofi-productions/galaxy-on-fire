using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private GameObject _playerExplosionPrefab;

    [SerializeField]
    private GameObject _shieldGameObject;


    [SerializeField]
    private float _speed = 5.0f;

    private const float _LIMIT_UP = 0f;
    private const float _LIMIT_DOWN = -4f;
    private const float _LIMIT_LEFT = -7f;
    private const float _LIMIT_RIGHT = 7f;
    private const float _LASER_POSITION = 0.9f;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = 0.0f;

    public bool canTripleShot = false;
    public bool shieldPowerup = false;

    public int lives = 3;

    
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

        if (Input.GetKeyDown(KeyCode.Space) || (Input.GetMouseButton(0)))
        {
            Shoot();
        }
    }

    public void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
        transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);

        if (transform.position.y > _LIMIT_UP)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }

        else if (transform.position.y < _LIMIT_DOWN)
        {
            transform.position = new Vector3(transform.position.x, _LIMIT_DOWN, 0);
        }

        else if (transform.position.x > 9.5f)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }

        else if (transform.position.x < -9.5f)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }
    }

    private void Shoot()
    {
        
        if (Time.time > _canFire)//condition time to next shoot (pay attention line 32)
        {
            if (canTripleShot == true)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, _LASER_POSITION, 0), Quaternion.identity);
            }
            _canFire = Time.time + _fireRate;
        }
        
    }

    public void Damage()
    {
        if (shieldPowerup != true)
        {
            lives--;
            if (lives == 0)
            {
                Instantiate(_playerExplosionPrefab, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
        else
        {
            shieldPowerup = false;
            _shieldGameObject.SetActive(false);
        }
    }

    public void TripleShotPowerupOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }
    public void IncreaseSpeedOn()
    {
        _speed = 10.0f;
        StartCoroutine(IncreaseSpeed());
    }

    public IEnumerator IncreaseSpeed()
    {
        yield return new WaitForSeconds(5.0f);
        _speed = 5.0f;
    }

    public void ShieldOn()
    {
    
        shieldPowerup = true;
        _shieldGameObject.SetActive(true);

    }
}