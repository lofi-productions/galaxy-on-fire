using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private const float LASER_POSITION_DESTROY = 5.5f;
    [SerializeField]
    private float _speed = 10.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        if (transform.position.y >= LASER_POSITION_DESTROY)
        {
            DestroyGameObject();
        }

    }

    void DestroyGameObject()
    {
        Destroy(this.gameObject);
    }
}
