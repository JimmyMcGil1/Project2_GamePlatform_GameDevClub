using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnightStatic : MonoBehaviour, IDataPersistence
{
    public int currHeal;
    public int currEXP;
    public int currLevel;
    public int maxEXP;
    [SerializeField] Slider slider_heal;
    [SerializeField] Slider slider_exp;
    [SerializeField] Text heal_text;
    [SerializeField] Text exp_text;
    [SerializeField] Text level_text;
    int maxHeal;
    public static KnightStatic instance { get; private set; }
    Animator anim;
    Rigidbody2D rigit;
    private void Awake()
    {
        instance = this;
        maxHeal = 100;
        maxEXP = 200;
        slider_heal.maxValue = maxHeal;
        slider_heal.value = currHeal;
        slider_exp.maxValue = maxEXP;
        slider_exp.value = currEXP;
        heal_text.text = $"{currHeal}/{maxHeal}";
        exp_text.text = $"{currEXP}/{maxEXP}";

        anim = GetComponent<Animator>();
        rigit = GetComponent<Rigidbody2D>();
    }
   
    void HealChange(int healChange)
    {
        if (healChange < 0)
            currHeal = currHeal + healChange < 0 ? 0 : currHeal + healChange;
        else
            currHeal = currHeal + healChange > maxHeal ? maxHeal : currHeal + healChange;
        slider_heal.value = currHeal;
        heal_text.text = $"{currHeal}/{maxHeal}";
    }
    private void Start()
    {
          slider_heal.value = currHeal;
        heal_text.text = $"{currHeal}/{maxHeal}";
        slider_exp.value = currEXP;
        exp_text.text = $"{currEXP}/{maxEXP}";
        level_text.text = $"Lv: {currLevel}";
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) TakeDame(-10);
        if (Input.GetKeyDown(KeyCode.F)) HealChange(10);
        if (Input.GetKeyDown(KeyCode.R)) GainEXP(10);
      
    }
    /// <summary>
    /// Knight nhan vao luong sat thuong = dame. Luu y dame phai be hon 0
    /// </summary>
    /// <param name="dame"></param>
    public void TakeDame(int dame)
    {
        HealChange(dame);
        anim.SetTrigger("hit");
        rigit.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * 4, 0), ForceMode2D.Impulse);
        if (currHeal == 0)
        {
            anim.SetTrigger("dead");
        }
    }
    private void LevelUp()
    {
        currLevel++;
        currEXP = 0;
        exp_text.text = $"{currEXP}/{maxEXP}";
        level_text.text = $"Lv: {currLevel}";
        slider_exp.value = currEXP;
    }
    /// <summary>
    /// Nguoi choi duoc tang EXP. Luu y luong EXP tang phai lon hon 0 va be hon 200.
    /// </summary>
    /// <param name="bonusExp"></param>
    public void GainEXP(int bonusExp)
    {
        currEXP = (currEXP + bonusExp > maxEXP) ? (maxEXP) : (currEXP + bonusExp);
        exp_text.text = $"{currEXP}/{maxEXP}";
        slider_exp.value = currEXP;
        if (currEXP == 200) LevelUp();
    }
    public void LoadData(GameData gameData)
    {
        currHeal = gameData.HP;
        currEXP = gameData.EXP;
        currLevel = gameData.Level;
        Debug.Log($"Load HP = {gameData.HP}");
    }
    public void SaveData(ref GameData gameData)
    {
        gameData.EXP = currEXP;
        gameData.Level = currLevel;
        gameData.HP = currHeal;
    }
    
}
