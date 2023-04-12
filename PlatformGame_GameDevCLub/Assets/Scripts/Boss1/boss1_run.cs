using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss1_run : StateMachineBehaviour
{

    Transform knightPos;
    [SerializeField] float speed;
    [SerializeField] float attackRange;
    float attack1_timmer;
    float attack1_counter;

    Rigidbody2D rigit;
    boss1_beheviour boss;
    [SerializeField] float amplitude;
    float y0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        knightPos = GameObject.FindGameObjectWithTag("Knight").transform;
        rigit = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<boss1_beheviour>();
        attack1_timmer = 1;
        attack1_counter = 0;
        y0 = rigit.position.y;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if ((knightPos.position.x - boss.transform.position.x) * boss.transform.localScale.x < 0 )
        {
            Vector2 faceDir = boss.transform.localScale;
            faceDir.x *= -1;
            boss.transform.localScale = faceDir;

        }
        Vector2 target = new Vector2(knightPos.position.x, rigit.position.y);
        target.y = y0 + amplitude * Mathf.Sin(speed * Time.time);

        Vector2 newPos = Vector2.MoveTowards(rigit.position,target, Time.fixedDeltaTime * speed);
        rigit.MovePosition(newPos);
        if (Mathf.Abs(knightPos.position.x - boss.transform.position.x)  < attackRange) {
            if (attack1_counter > attack1_timmer)
            {
                if (boss.transform.localScale.x > 0) animator.SetTrigger("attack_1");
                else animator.SetTrigger("attack_1_left");
                attack1_counter = 0;
            }
        }
        attack1_counter += Time.deltaTime;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("attack_1");
    }










    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
