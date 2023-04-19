using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss1_static : MonoBehaviour
{
    public static  Boss1_static instance { get; private set; }
    public float nonHurtTimmer;
    float nonHurtCounter;
    public int currHeal { get; private set; }
    [SerializeField] Text heal_text;
    [SerializeField] Slider slider_heal;
    public int maxHeal;
    Animator anim;
    Rigidbody2D rigit;
    bool isSummon;
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;

        currHeal = maxHeal;
        slider_heal.maxValue = maxHeal;
        slider_heal.value = currHeal;
        anim = GetComponent<Animator>();
        rigit = GetComponent<Rigidbody2D>();
        heal_text.text = $"{currHeal}/{maxHeal}";
        nonHurtCounter = Mathf.Infinity;
        isSummon = false;
    }
    public void TakeDame(int dame)
    {
        if (nonHurtCounter > nonHurtTimmer)
        {
            currHeal = (currHeal + dame < 0) ? 0 : currHeal + dame;
            if (currHeal == 0) anim.SetTrigger("death");
            else anim.SetTrigger("hurt");
            slider_heal.value = currHeal;
            heal_text.text = $"{currHeal}/{maxHeal}";
            nonHurtCounter = 0;
        }
        if (currHeal <= maxHeal * 0.6f)
        {
            boss1_beheviour.changeState = true;
            anim.SetBool("run_state_1", false);
            anim.SetBool("run_state_2", true);
        }

    }
    void Death()
    {
        Destroy(gameObject);
    }
    private void Update()
    {
        nonHurtCounter += Time.deltaTime;
    }
}
