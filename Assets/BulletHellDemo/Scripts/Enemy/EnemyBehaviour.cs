using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("All states data")]
    [SerializeField]
    private Animator _animator;

    [Space]
    [Header("Idle state data")]
    [SerializeField]
    private float _minIdleStateTime = 1;
    [SerializeField]
    private float _maxIdleStateTime = 2;
    [SerializeField]
    private float _walkProbability = 1;
    [SerializeField]
    private float _attack1Probability = 1;
    [SerializeField]
    private float _attack2Probability = 1;

    [Space]
    [Header("Walk state data")]
    [SerializeField]
    private float _minWalkStateTime = 1;
    [SerializeField]
    private float _maxWalkStateTime = 2;
    [SerializeField]
    private Transform _targetTransform;
    [SerializeField]
    private float _speed = 0.02f;
    [SerializeField]
    private float _minDistance = 1f;

    [Space]
    [Header("Attack state data")]
    [SerializeField]
    private float _minAttack1StateTime = 1;
    [SerializeField]
    private float _maxAttack1StateTime = 2;
    [SerializeField]
    private GameObject _bullet;
    [SerializeField]
    private Transform[] _points;
    [SerializeField]
    private float _bulletSpeed = 1000;
    [SerializeField]
    private float _timerBulletGeneration;
    [SerializeField]
    private AttackAnimation _attackAnimation;
    [SerializeField]
    private Patterns _patterns;
    

    private Fsm.Fsm _fsm = new Fsm.Fsm();

    public delegate GameObject OnInstantiate(GameObject gameObject);
    public OnInstantiate InstantiateAction { get; private set; }

    private void Awake()
    {
        _fsm.AddState(new IdleState(_fsm, _minIdleStateTime, _maxIdleStateTime,
            _walkProbability, _attack1Probability, _attack2Probability));
        _fsm.AddState(new WalkState(_fsm, _minWalkStateTime, _maxWalkStateTime,
            _animator, transform, _targetTransform, _speed, _minDistance));
        _fsm.AddState(new Attack1(_fsm, this,
            _minAttack1StateTime, _maxAttack1StateTime, _animator,
            _bullet, _points, transform, _bulletSpeed, _timerBulletGeneration,
            _attackAnimation, _patterns.GetCurvedPatterns()[0]));
        _fsm.AddState(new Attack2(_fsm, this,
            _minAttack1StateTime, _maxAttack1StateTime, _animator,
            _bullet, _points, transform, _bulletSpeed, _timerBulletGeneration,
            _attackAnimation, _patterns.GetCurvedPatterns()[1]));
        _fsm.SetState<IdleState>();
    }

    private void OnEnable()
    {
        InstantiateAction += InstantiateObject;
    }

    private void OnDisable()
    {
        InstantiateAction -= InstantiateObject;
    }

    private void FixedUpdate()
    {
        _fsm?.FixedUpdate();
    }

    private GameObject InstantiateObject(GameObject gameObject)
    {
        return Instantiate(gameObject);
    }
}
