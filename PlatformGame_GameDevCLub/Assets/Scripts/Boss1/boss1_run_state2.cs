using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss1_run_state2 : StateMachineBehaviour
{
    Transform knightPos;
    [SerializeField] float speed;
    [SerializeField] float attackRange;
    float skill_timmer;
    float skill_counter;

    float attack_2_timmer;
    float attack_2_counter;
    boss1_attack attacker;

    Rigidbody2D rigit;
    boss1_beheviour boss;
    [SerializeField] float amplitude;
    float y0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        knightPos = GameObject.FindGameObjectWithTag("Knight").transform;
        attacker = animator.GetComponent<boss1_attack>();
        rigit = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<boss1_beheviour>();
        skill_timmer = 3;
        skill_counter = 0;
         attack_2_timmer = 0.7f;
         attack_2_counter = 0f;

        y0 = rigit.position.y;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if ((knightPos.position.x - boss.transform.position.x) * boss.transform.localScale.x < 0)
        {
            Vector2 faceDir = boss.transform.localScale;
            faceDir.x *= -1;
            boss.transform.localScale = faceDir;

        }
        Vector2 target = new Vector2(knightPos.position.x, rigit.position.y);
        target.y = y0 + amplitude * Mathf.Sin(speed * Time.time);
        Vector2 newPos = Vector2.MoveTowards(rigit.position, target, Time.fixedDeltaTime * speed);
        rigit.MovePosition(newPos);
            if (skill_counter > skill_timmer)
            {
            animator.SetTrigger("skill");
            attacker.Skill_Attack(knightPos);
                skill_counter = 0;
            }
        if (Mathf.Abs(knightPos.position.x - boss.transform.position.x) < attackRange)
        {
            if (attack_2_counter > attack_2_timmer)
            {
                animator.SetTrigger("attack_2");
                attack_2_counter = 0;
            }
        }
        skill_counter += Time.deltaTime;
        attack_2_counter += Time.deltaTime;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("attack_2");
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
