using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts
{
	public sealed class CharacterAnimation : MonoBehaviour
	{
		private CharacterAnimationState _animationState = CharacterAnimationState.None;

		[SerializeField] private Animator _animator;

		private enum CharacterAnimationState
		{
			Left,

			Right,

			None
		};

		public void Update()
		{
			float horizontal = Input.GetAxis("Horizontal");
			bool pressingLeft = horizontal < -Mathf.Epsilon;
			bool pressingRight = horizontal > Mathf.Epsilon;
			bool notPressingLeftOrRight = !pressingLeft && !pressingRight;

			if (notPressingLeftOrRight)
			{
				UpdateAnimationState(CharacterAnimationState.None);
			}
			else if (pressingLeft)
			{
				UpdateAnimationState(CharacterAnimationState.Left);
			}
			else if (pressingRight)
			{
				UpdateAnimationState(CharacterAnimationState.Right);
			}
		}

		private void UpdateAnimationState(CharacterAnimationState newState)
		{
			if (newState != _animationState)
			{
				switch (newState)
				{
					case CharacterAnimationState.Left:
						_animator.Play("WalkLeft");
						break;

					case CharacterAnimationState.Right:
						_animator.Play("WalkRight");
						break;

					case CharacterAnimationState.None:
						_animator.Play("Idle");
						break;
				}
			}
			_animationState = newState;
		}
	}
}
