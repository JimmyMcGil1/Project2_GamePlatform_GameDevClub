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
        
    }
    
    public void TakeDame(int dmg)
    {

        if (currHeal + dmg <= 0) Dead();
        else
        {
        //    anim.SetTrigger("hit");
       
            transform.Translate(new Vector2(repel * Mathf.Sign(transform.localPosition.x) * Time.deltaTime, 0f));
            currHeal += dmg;

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) TakeDame(-10);
        heal_slider.value = currHeal;
        heal_text.text = $"{currHeal}/{totalHeal}";
    }
    void Dead()
    {

        gameObject.SetActive(false);

    }


}
