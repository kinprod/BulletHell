using Fsm;
using System.Threading.Tasks;
using UnityEngine;

public class Attack2 : FsmState
{
    private EnemyBehaviour _enemyBehaviour;

    private float _minStateTime;
    private float _maxStateTime;

    private Animator _animator;

    private GameObject _bullet;
    private Transform[] _points;
    private Transform _transform;

    private float _bulletSpeed;
    private float _timerToGenerate;

    private float _timeOnState;

    private Task _backIdleTask;
    private Task _bulletGeneration;

    private AttackAnimation _attackAnimation;
    private AnimationCurve _attackPattern;

    public Attack2(Fsm.Fsm fsm, EnemyBehaviour enemyBehaviour,
        float minStateTime, float maxStateTime, Animator animator,
        GameObject bullet, Transform[] points, Transform transform,
        float bulletSpeed, float timerToGenerate,
        AttackAnimation attackAnimation, AnimationCurve animationCurve) : base(fsm)
    {
        _enemyBehaviour = enemyBehaviour;

        _minStateTime = minStateTime;
        _maxStateTime = maxStateTime;

        _animator = animator;

        _bullet = bullet;
        _points = points;
        _transform = transform;

        _bulletSpeed = bulletSpeed;
        _timerToGenerate = timerToGenerate;

        _attackAnimation = attackAnimation;
        _attackPattern = animationCurve;
    }

    public override void Enter()
    {
        _timeOnState = _minStateTime;
        TimeOnState();
        _backIdleTask = BackIdle();
        _bulletGeneration = BulletTimer();
        _animator.SetBool("Attack2", true);
    }

    public override void Exit()
    {
        _animator.SetBool("Attack2", false);
    }

    public override void FixedUpdate()
    {
        _attackAnimation.UpdateRotation(_attackPattern);
        if (_bulletGeneration.IsCompleted)
        {
            _bulletGeneration = BulletTimer();
        }
    }

    private async Task BackIdle()
    {
        await Task.Delay((int)_timeOnState * 1000);
        Fsm.SetState<IdleState>();
    }

    private async Task BulletTimer()
    {
        foreach (Transform point in _points)
        {
            Generation(point);
        }
        await Task.Delay((int)(_timerToGenerate * 1000));
    }

    private void Generation(Transform point)
    {
        GameObject bullet = _enemyBehaviour.InstantiateAction(_bullet);
        bullet.transform.position = point.position;
        Rigidbody2D rigidbody2D = bullet.GetComponent<Rigidbody2D>();
        rigidbody2D.AddForce((point.position - _transform.position).normalized * _bulletSpeed);
        Debug.Log((point.position - _transform.position).normalized * _bulletSpeed);
    }

    private void TimeOnState()
    {
        _timeOnState = _minStateTime + Random.Range(0, Mathf.Abs(_maxStateTime - _minStateTime));
    }
}
