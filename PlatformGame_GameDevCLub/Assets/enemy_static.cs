using UnityEngine;
using UnityEngine.UI;

public class enemy_static : MonoBehaviour
{
    [SerializeField] int totalHeal = 100;
    int currHeal;
    public float repel;
    Animator anim;
    BoxCollider2D box;
    Color oldColor;
    Color disColor;
    Slider heal_slider;
    Text heal_text;
    public float nonHurtTimmer;
    float nonHurtCounter;
    [SerializeField] AudioSource DeathSoundEffect;
    [SerializeField] AudioSource HitSoundEffect;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
        disColor = new Color(oldColor.r, oldColor.g, oldColor.b, 0);
        heal_slider = gameObject.transform.Find("Heal_UI").Find("Heal_slider").gameObject.GetComponent<Slider>();
        heal_text = gameObject.transform.Find("Heal_UI").Find("Heal_slider").Find("Heal_text").gameObject.GetComponent<Text>();
        heal_slider.maxValue = totalHeal;
        currHeal = totalHeal;
        heal_slider.value = currHeal;
        heal_text.text = $"{currHeal}/{totalHeal}";
        nonHurtCounter = Mathf.Infinity;
        
    }
    
    public void TakeDame(int dmg)
    {

        if (currHeal + dmg <= 0)
        {
            currHeal = 0;
            anim.SetTrigger("death");
            DeathSoundEffect.Play();
            return;
        }
        else
        {
            if (nonHurtCounter < nonHurtTimmer) return;
             anim.SetTrigger("hit");
            HitSoundEffect.Play();
             currHeal += dmg;
             nonHurtCounter = 0;

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) TakeDame(-10);
        heal_slider.value = currHeal;
        heal_text.text = $"{currHeal}/{totalHeal}";
        nonHurtCounter += Time.deltaTime;
    }
    void Dead()
    {
        Destroy(gameObject);
    }


}
