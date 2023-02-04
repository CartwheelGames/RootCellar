using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts
{
	public sealed class GameManager : MonoBehaviour
	{
		public AppStateManager AppStateManager { get; } = new();
	}
}
