using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Walk() {
        animator.SetBool("isWalking", true);
    }

    public void Stop() {
        animator.SetBool("isWalking", false);
    }

    public void Run() {
        animator.SetBool("isRunning", true);
    }

    public void StopRun() {
        animator.SetBool("isRunning", false);
    }

    public void Roll() {
        animator.SetBool("isRolling", true);
    }

    public void StopRoll() {
        animator.SetBool("isRolling", false);
    }

    public void Jump() {
        animator.SetTrigger("Jump");
    }

    public void GameStarted() {
        animator.SetTrigger("GameStarted");
    }
}
