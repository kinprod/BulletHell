using Fsm;
using System.Threading.Tasks;
using UnityEngine;

public class WalkState : FsmState
{
    private float _minStateTime;
    private float _maxStateTime;

    private Animator _animator;
    private Transform _transform;
    private Transform _targetTransform;

    private float _speed;
    private float _minDistance;

    private float _timeOnState;
    private Task _task;

    public WalkState(Fsm.Fsm fsm, float minStateTime, float maxStateTime,
        Animator animator, Transform transform, Transform targetTransform,
        float speed, float minDistance) : base(fsm)
    {
        _minStateTime = minStateTime;
        _maxStateTime = maxStateTime;

        _animator = animator;
        _transform = transform;
        _targetTransform = targetTransform;

        _speed = speed;
        _minDistance = minDistance;
    }

    public override void Enter()
    {
        _timeOnState = _minStateTime;
        TimeOnState();
        _task = BackIdle();
        _animator.SetBool("Walk", true);
    }

    public override void FixedUpdate()
    {
        Move();
    }

    public override void Exit()
    {
        _animator.SetBool("Walk", false);
    }

    private async Task BackIdle()
    {
        await Task.Delay((int)(_timeOnState * 1000));
        FSM.SetState<IdleState>();
    }

    private void Move()
    {
        Vector2 direction = (_targetTransform.position - _transform.position).normalized;
        if ((_targetTransform.position - _transform.position).magnitude > _minDistance)
        {
            _transform.position += new Vector3(direction.x, direction.y) * _speed;
        }
    }

    private void TimeOnState()
    {
        _timeOnState = _minStateTime + Random.Range(0, Mathf.Abs(_maxStateTime - _minStateTime));
    }
}
