using AssemblyCSharp.AssetsData.Data.Config;
using AssemblyCSharp.AssetsData.Data.State;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts
{
	public sealed class CameraMovement : MonoBehaviour
	{
		private Character character;

		private CharacterConfig characterConfig;

		public void Initialize(Character character, CharacterConfig characterConfig)
		{
			this.character = character;
			this.characterConfig = characterConfig;
		}

		public void Update()
		{
			if (character != null && characterConfig != null)
			{
				float offset = characterConfig.cameraOffset;
				if (character.IsFacingLeft)
				{
					offset *= -1;
				}
				Vector3 target = new(character.X + offset, transform.position.y, transform.position.z);
				transform.position = Vector3.Lerp(transform.position, target, characterConfig.cameraSpeed * Time.deltaTime);
			}
		}
	}
}
