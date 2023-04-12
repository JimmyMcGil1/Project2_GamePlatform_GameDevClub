using UnityEngine;
using UnityEngine.UI;

public class UI_LevelUp : MonoBehaviour
{
    Text gainAttack;
    Text gainStrength;
    Text gainSpeed;
    int bonusAttack;
    int bonusStrength;
    int bonusSpeed;
    private void Awake()
    {
        gainAttack = gameObject.transform.Find("Up_Attack").gameObject.transform.Find("attack_text").gameObject.GetComponent<Text>();
        gainStrength = gameObject.transform.Find("Up_Strength").gameObject.transform.Find("strength_text").gameObject.GetComponent<Text>();
        gainSpeed = gameObject.transform.Find("Up_Speed").gameObject.transform.Find("speed_text").gameObject.GetComponent<Text>();
       

    }
    private void Start()
    {
        bonusAttack = Mathf.CeilToInt(KnightStatic.instance.attackPower * 0.5f);
        bonusStrength = Mathf.CeilToInt(KnightStatic.instance.strength * 0.5f);
        bonusSpeed = Mathf.CeilToInt(KnightStatic.instance.speed * 0.1f);

        gainAttack.text = $"+{bonusAttack}";
        gainStrength.text = $"+{bonusStrength}";
        gainSpeed.text =  $"+{bonusSpeed}";
    }
    public void ChooseUpAttack()
    {
        Debug.Log("choose attack");
        KnightStatic.instance.attackPower += bonusAttack;
        gameObject.SetActive(false);
    }
    public void ChooseUpStrength()
    {
        Debug.Log("choose strength");
        KnightStatic.instance.strength += bonusStrength;
        UI_static_present.UpdateStatic();
        gameObject.SetActive(false);
    }
    public void ChooseUpSpeed()
    {

        Debug.Log("choose speed");
        KnightStatic.instance.speed += bonusSpeed;
        UI_static_present.UpdateStatic();
        gameObject.SetActive(false);
    }
}
