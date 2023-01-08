using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CancelAnimator : MonoBehaviour {
    private Animator animator;
    private void Start() {
        animator = GetComponent<Animator>();
    }
    public void StopAnimator() {
        animator.enabled = false;
    }
}
