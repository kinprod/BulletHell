using System.Collections;
using System.Drawing;
using UnityEngine;

public class BulletGeneration : MonoBehaviour
{
    [SerializeField]
    private GameObject _bullet;
    [SerializeField]
    private Transform[] _points;

    [Space]
    [SerializeField]
    private float _force = 0;
    [SerializeField]
    private float _timer = 1;

    private float _time = 0;

    private void Update()
    {
        if (_time < _timer)
        {
            _time += Time.deltaTime;
        }
        else
        {
            _time = 0;
            foreach (Transform point in _points)
            {
                Generation(point);
            }
        }
    }


    private void Generation(Transform point)
    {
        GameObject bullet = Instantiate(_bullet);
        bullet.transform.position = point.position;
        Rigidbody2D rigidbody2D = bullet.GetComponent<Rigidbody2D>();
        rigidbody2D.AddForce((point.position - transform.position).normalized * _force);
    }
}
