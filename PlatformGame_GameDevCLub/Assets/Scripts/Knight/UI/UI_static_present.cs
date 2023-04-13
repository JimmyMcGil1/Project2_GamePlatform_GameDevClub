using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_static_present : MonoBehaviour
{
    static Text attack_text;
    static Text strength_text;
    static Text speed_text;
    static Text heal_text;
    static GameObject flash;
    static GameObject shot;
    private void Awake()
    {
        attack_text = gameObject.transform.Find("Attack").gameObject.transform.Find("attack_text").gameObject.GetComponent<Text>();
        strength_text = gameObject.transform.Find("Strength").gameObject.transform.Find("strength_text").gameObject.GetComponent<Text>();
        speed_text = gameObject.transform.Find("Speed").gameObject.transform.Find("speed_text").gameObject.GetComponent<Text>();
        heal_text = gameObject.transform.Find("Heal").gameObject.transform.Find("heal_text").gameObject.GetComponent<Text>();
        flash = gameObject.transform.Find("Flash_Skill").gameObject;
        shot = gameObject.transform.Find("Skill_Shot").gameObject;
    }
    private void Start()
    {
        UpdateStatic();
    }
    private void Update()
    {
        UpdateStatic();
    }
    public static void UpdateStatic()
    {
        attack_text.text = KnightStatic.instance.attackPower.ToString();
        strength_text.text = KnightStatic.instance.strength.ToString();
        speed_text.text = KnightStatic.instance.speed.ToString();
        flash.SetActive(KnightStatic.instance.canFlash == 1);
        shot.SetActive(KnightStatic.instance.canShot == 1);
        heal_text.text = KnightStatic.instance.maxHeal.ToString();
    }
   
}
