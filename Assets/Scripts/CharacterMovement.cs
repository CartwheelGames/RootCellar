using AssemblyCSharp.AssetsData.Data.Config;
using AssemblyCSharp.AssetsData.Data.State;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts
{
	public sealed class CharacterMovement : MonoBehaviour
	{
		private Character character;

		private float speed = 1f;

		public void Initialize(CharacterConfig characterConfig, Character character)
		{
			speed = characterConfig.baseSpeed;
			this.character = character;
		}

		public void Update()
		{
			if (character != null)
			{
				float horizontalInput = Input.GetAxis("Horizontal");
				float verticalInput = Input.GetAxis("Vertical");
				if (horizontalInput < -float.Epsilon)
				{
					transform.Translate(Vector2.left * speed);
					character.IsFacingLeft = true;
				}
				else if (horizontalInput > float.Epsilon)
				{
					transform.Translate(Vector2.right * speed);
					character.IsFacingLeft = false;
				}
				else if (verticalInput < -float.Epsilon)
				{
					Debug.Log("Down");
				}

				character.X = transform.position.x;
			}
		}
	}
}
