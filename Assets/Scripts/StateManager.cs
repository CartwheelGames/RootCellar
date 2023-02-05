using System;
using System.Collections.Generic;

namespace AssemblyCSharp.Assets.Scripts
{
	public abstract class StateManager<T> where T : Enum
	{
		public Dictionary<T, List<Action>> enterCallbacks = new();

		public Dictionary<T, List<Action>> leaveCallbacks = new();

		public T CurrentState { get; private set; }

		public void AddEnterListener(T state, Action callback)
		{
			if (!enterCallbacks.ContainsKey(state))
			{
				enterCallbacks.Add(state, new List<Action>());
			}
			enterCallbacks[state].Add(callback);
		}

		public void AddLeaveListener(T state, Action callback)
		{
			if (!leaveCallbacks.ContainsKey(state))
			{
				leaveCallbacks.Add(state, new List<Action>());
			}
			leaveCallbacks[state].Add(callback);
		}

		public void ChangeState(T newState)
		{
			if (!CurrentState.Equals(newState))
			{
				if (leaveCallbacks.TryGetValue(CurrentState, out List<Action> leaveActions))
				{
					foreach (Action action in leaveActions)
					{
						action.Invoke();
					}
				}
				CurrentState = newState;
				if (enterCallbacks.TryGetValue(CurrentState, out List<Action> enterActions))
				{
					foreach (Action action in enterActions)
					{
						action.Invoke();
					}
				}
			}
		}
	}
}
