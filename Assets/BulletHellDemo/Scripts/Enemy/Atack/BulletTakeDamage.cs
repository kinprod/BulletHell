using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class BulletTakeDamage : MonoBehaviour
{
    [SerializeField]
    private int _damage = 1;
    [SerializeField]
    private float _lifeTime = 20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            if (collision.tag == "Player")
            {
                damageable.TakeDamage(_damage);
            }
        }
        Destroy(gameObject);
    }

    private void Update()
    {
        if (_lifeTime > 0)
        {
            _lifeTime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
