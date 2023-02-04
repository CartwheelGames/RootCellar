using AssemblyCSharp.Assets.Data;
using System;
using System.Collections.Generic;

namespace AssemblyCSharp.Assets.Scripts
{
	public sealed class AppStateManager
	{
		public Dictionary<AppState, List<Action>> enterCallbacks = new();

		public Dictionary<AppState, List<Action>> leaveCallbacks = new();

		public AppState CurrentAppState { get; private set; }

		public void AddEnterListener(AppState appState, Action callback)
		{
			if (!enterCallbacks.ContainsKey(appState))
			{
				enterCallbacks.Add(appState, new List<Action>());
			}
			enterCallbacks[appState].Add(callback);
		}

		public void AddLeaveListener(AppState appState, Action callback)
		{
			if (!leaveCallbacks.ContainsKey(appState))
			{
				leaveCallbacks.Add(appState, new List<Action>());
			}
			leaveCallbacks[appState].Add(callback);
		}

		public void ChangeState(AppState newState)
		{
			if (CurrentAppState != newState)
			{
				if (leaveCallbacks.TryGetValue(CurrentAppState, out List<Action> leaveActions))
				{
					foreach(Action action in leaveActions)
					{
						action.Invoke();
					}
				}
				CurrentAppState = newState;
				if (enterCallbacks.TryGetValue(CurrentAppState, out List<Action> enterActions))
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
