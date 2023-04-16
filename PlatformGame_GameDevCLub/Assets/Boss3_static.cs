using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss3_static : MonoBehaviour
{
    public static Boss3_static instance { get; private set; }
    public int currHeal { get; private set; }
    [SerializeField] Text heal_text;
    [SerializeField] Slider slider_heal;
    public int maxHeal;
    Animator anim;
    Rigidbody2D rigit;
    public float nonHurtTimmer;
    float nonHurtCounter;
    public static bool fury;
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
        fury = false;
    }
    public void TakeDame(int dame)
    {
        if (nonHurtCounter > nonHurtTimmer)
        {
            currHeal = (currHeal + dame < 0) ? 0 : currHeal + dame;
            rigit.AddForce(Vector2.right * -Mathf.Sign(transform.localScale.x) * 1, ForceMode2D.Impulse);
            if (currHeal == 0) anim.SetTrigger("death");
            else anim.SetTrigger("hurt");
            slider_heal.value = currHeal;
            heal_text.text = $"{currHeal}/{maxHeal}";
            nonHurtCounter = 0;
        }
        if (currHeal < maxHeal * 0.8f) fury = true;
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
