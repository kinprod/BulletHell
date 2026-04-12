using UnityEditor;
using UnityEngine;

namespace Fsm
{
    public abstract class FsmState
    {
        protected readonly Fsm @Fsm;

        public FsmState(Fsm fsm)
        {
            @Fsm = fsm;
        }

        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
        public virtual void Exit() { }

    }
}
