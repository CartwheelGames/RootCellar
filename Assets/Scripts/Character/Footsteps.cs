using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts.Character
{
	public class Footsteps : StateMachineBehaviour
	{
		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			if (!animator.gameObject.GetComponent<AudioSource>().isPlaying)
			{
				animator.gameObject.GetComponent<AudioSource>().Play();
			}
		}

		// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
		//override public void OnStateEnter(Animator canvasGroup, AnimatorStateInfo stateInfo, int layerIndex)
		//{
		//
		//}

		// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
		//override public void OnStateUpdate(Animator canvasGroup, AnimatorStateInfo stateInfo, int layerIndex)
		//{
		//
		//}

		// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
		//override public void OnStateExit(Animator canvasGroup, AnimatorStateInfo stateInfo, int layerIndex)
		//{
		//
		//}

		// OnStateMove is called right after Animator.OnAnimatorMove()
		//override public void OnStateMove(Animator canvasGroup, AnimatorStateInfo stateInfo, int layerIndex)
		//{
		//    // Implement code that processes and affects root motion
		//}

		// OnStateIK is called right after Animator.OnAnimatorIK()
		//override public void OnStateIK(Animator canvasGroup, AnimatorStateInfo stateInfo, int layerIndex)
		//{
		//    // Implement code that sets up animation IK (inverse kinematics)
		//}
	}
}
