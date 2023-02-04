using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GGJ {
    public class StartScreenController : MonoBehaviour {
        
        [SerializeField] private GameObject entireScreen;
        [SerializeField] private Animator animator;
        
        public void Awake(){

            animator.Play("OnIdle");
            entireScreen.SetActive(true);
        }

        public void OnPlayButtonPress(){

            animator.Play("OnStartButtonClicked", 0);

            StartCoroutine(WaitThenTurnOff());
        }

        private IEnumerator WaitThenTurnOff(){
            
            yield return new WaitForSeconds(10);
            
            MakeInvisible();
        }

        public void MakeInvisible(){
        
            entireScreen.SetActive(false);
        }
    }
}
