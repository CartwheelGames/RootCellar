using AssemblyCSharp.AssetsData.Data.State;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts
{
	public sealed class CameraMovement : MonoBehaviour
	{
		public CharacterMovement characterMovement;

		private Character character;

		public void Initialize(CharacterMovement characterMovement, Character character)
		{
			this.characterMovement = characterMovement;
			this.character = character;
		}
	}
}
