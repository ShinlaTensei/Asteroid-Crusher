using System;

namespace Pattern.Implement
{
    public abstract class State
    {
        public event Action<State> OnStateEnter;
        public Action<State> onStateExit;

        ~State()
        {
            OnStateEnter = null;
        }
        public virtual void Enter()
        {
            
        }

        public virtual void Exit()
        {
            
        }

        public virtual void Update()
        {
            
        }

        public void OnEnter(State state)
        {
            OnStateEnter?.Invoke(state);
        }
    }
}