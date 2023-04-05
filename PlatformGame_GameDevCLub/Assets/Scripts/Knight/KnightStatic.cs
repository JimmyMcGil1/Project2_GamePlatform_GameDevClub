using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnightStatic : MonoBehaviour
{
    float currHeal;
    [SerializeField] Slider slider;
    [SerializeField] Text heal_text;
    float maxHeal;
    public static KnightStatic instance { get; private set; }
    Animator anim;
    Rigidbody2D rigit;
    private void Awake()
    {
        instance = this;
        maxHeal = 100;
        currHeal = maxHeal;
        slider.maxValue = maxHeal;
        slider.value = currHeal;
        heal_text.text = $"{currHeal}/{maxHeal}";

        anim = GetComponent<Animator>();
        rigit = GetComponent<Rigidbody2D>();
    }
    void HealChange(float healChange)
    {
        if (healChange < 0)
            currHeal = currHeal + healChange < 0 ? currHeal : currHeal + healChange;
        else
            currHeal = currHeal + healChange > maxHeal ? currHeal : currHeal + healChange;
        slider.value = currHeal;
        heal_text.text = $"{currHeal}/{maxHeal}";
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) TakeDame(-10);
        if (Input.GetKeyDown(KeyCode.F)) HealChange(10);
        
    }
    public void TakeDame(float dame)
    {
        HealChange(dame);
        anim.SetTrigger("hit");
        rigit.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * 4, 0), ForceMode2D.Impulse);
        if (currHeal == 0)
        {
            anim.SetTrigger("death");
        }

    }
}
