using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss2_wall_state : StateMachineBehaviour
{

    Transform knightPos;
    [SerializeField] float speed;
    [SerializeField] float attackRange;
    float dirMove;
    float attack1_timmer;
    float attack1_counter;
    int no;

    Rigidbody2D rigit;
    Boss2_behavior boss;
    float y0;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
      {
        knightPos = GameObject.FindGameObjectWithTag("Knight").transform;
        rigit = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss2_behavior>();
        attack1_timmer = 1;
        attack1_counter = 0;
        y0 = rigit.position.y;
        dirMove = 1;
        no = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if ((knightPos.position.x - boss.transform.position.x) * boss.transform.localScale.x > 0)
        {
            Vector2 faceDir = boss.transform.localScale;
            faceDir.x *= -1;
            boss.transform.localScale = faceDir;
        }
            Vector2 target = new Vector2(knightPos.position.x * dirMove, rigit.position.y);
            Vector2 newPos = Vector2.MoveTowards(rigit.position, target, Time.fixedDeltaTime * speed);
            rigit.MovePosition(newPos);
        
        if (Mathf.Abs(knightPos.position.x - boss.transform.position.x) < attackRange)
        {
           
                if (attack1_counter > attack1_timmer)
                {
                    if (no == 3)
                    {
                    animator.SetTrigger("attack_no");
                    no = 0;
                    }
                     else
                     {
                    no += 1;
                      animator.SetTrigger("attack_normal");
                      attack1_counter = 0;
                     }
                }
        }
        attack1_counter += Time.deltaTime;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
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
