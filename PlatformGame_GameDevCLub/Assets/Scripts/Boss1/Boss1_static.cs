using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss1_static : MonoBehaviour
{
    public static  Boss1_static instance { get; private set; }
    public int currHeal { get; private set; }
    [SerializeField] Text heal_text;
    [SerializeField] Slider slider_heal;
    public int maxHeal;
    Animator anim;
    Rigidbody2D rigit;
    private void Awake()
    {
        if (instance == null) instance = new Boss1_static();
        else if (this != instance) instance = this;

        currHeal = maxHeal;
        slider_heal.maxValue = maxHeal;
        slider_heal.value = currHeal;
        anim = GetComponent<Animator>();
        rigit = GetComponent<Rigidbody2D>();
        heal_text.text = $"{currHeal}/{maxHeal}";

    }
    public void TakeDame(int dame)
    {
        currHeal = (currHeal + dame < 0) ? 0 : currHeal + dame;
        rigit.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * 0.1f, 0), ForceMode2D.Force);
        slider_heal.value = currHeal;
        heal_text.text = $"{currHeal}/{maxHeal}";
        if (currHeal < maxHeal * 0.95f)
        {
            boss1_beheviour.changeState = true;
            anim.SetBool("run_state_1", false);
            anim.SetBool("run_state_2", true);
        }

    }
}
