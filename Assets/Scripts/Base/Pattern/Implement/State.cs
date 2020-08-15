using System;

namespace Base.Pattern.Implement
{
    public abstract class State
    {
        public virtual void Enter()
        {
            GameManager.Instance.gameStateMachine.OnEnter();
        }

        public virtual void Exit()
        {
            GameManager.Instance.gameStateMachine.OnExit();
        }

        public virtual void Update()
        {
            
        }
    }
}