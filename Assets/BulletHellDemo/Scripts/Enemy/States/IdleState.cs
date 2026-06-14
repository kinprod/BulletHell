using Fsm;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

public class IdleState : FsmState
{
    private float _minStateTime = 5;
    private float _maxStateTime = 5;

    private float _walkProbability;
    private float _attack1Probability;
    private float _attack2Probability;

    private float _timeOnState;
    private Task _task;

    public IdleState(Fsm.Fsm fsm, float minStateTime, float maxStateTime,
        float walkProbability, float attack1Probability, float attack2Probability) : base(fsm)
    {
        _minStateTime = minStateTime;
        _maxStateTime = maxStateTime;

        _walkProbability = walkProbability;
        _attack1Probability = attack1Probability;
        _attack2Probability = attack2Probability;
    }

    public override void Enter()
    {
        _timeOnState = _minStateTime;
        TimeOnState();
        _task = SwitchState();
    }

    public override void FixedUpdate()
    {
        if (_task.IsCompleted)
        {
            _task = SwitchState();
        }
    }

    private async Task SwitchState()
    {
        await Task.Delay((int)(_timeOnState * 1000));

        var random = Random.Range(0, _walkProbability + _attack1Probability + _attack2Probability);

        if (random <= _walkProbability)
        {
            Fsm.SetState<WalkState>();
        }
        else if (_walkProbability < random && random <= _walkProbability + _attack1Probability)
        {
            Fsm.SetState<Attack1>();
        }
        else if (_walkProbability + _attack1Probability < random && random <= _walkProbability + _attack1Probability + _attack2Probability)
        {
            Fsm.SetState<Attack2>();
        }
    }

    private void TimeOnState()
    {
        _timeOnState = _minStateTime + Random.Range(0, Mathf.Abs(_maxStateTime - _minStateTime));
    }
}

