using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnightStatic : MonoBehaviour, IDataPersistence
{

    [HideInInspector] public int currHeal;
    [HideInInspector] public int currEXP;
    [HideInInspector] public int currLevel;

    [HideInInspector] public int maxEXP;
    [HideInInspector] public int maxHeal = 100;
    [HideInInspector] public int attackPower;
    [HideInInspector] public int strength;
    [HideInInspector] public int speed;
    [HideInInspector] public int canFlash;
    [HideInInspector] public int canShot;
    GameObject UI_kngit_static;
    Slider slider_heal;
     Slider slider_exp;
     Text heal_text;
    Text exp_text;
     Text level_text;
    GameObject UI_static_present;
    GameObject UI_LevelUp;
    public static KnightStatic instance { get; private set; }
    Animator anim;
    Rigidbody2D rigit;
    bool firstLoad;
    public float nonHurtTimmer;
    float nonHurtCounter;
    [SerializeField] GameObject R;
    [SerializeField] GameObject M1;
    [SerializeField] AudioSource HitSoundEffect;
    [SerializeField] AudioSource DeathSoundEffect;
    [SerializeField] AudioSource LevelUpSoundEffect;
    public bool isDead;
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
        UI_kngit_static = GameObject.FindGameObjectWithTag("UI_knight_static");
        slider_heal = UI_kngit_static.transform.Find("Heal_Slider").gameObject.GetComponent<Slider>();
        slider_exp = UI_kngit_static.transform.Find("EXP_Slider").gameObject.GetComponent<Slider>();
        heal_text = slider_heal.transform.Find("Heal").gameObject.GetComponent<Text>();
        exp_text = slider_exp.transform.Find("EXP_Text").gameObject.GetComponent<Text>();
        level_text = UI_kngit_static.transform.Find("Level_Text").GetComponent<Text>();
        firstLoad = false;
        instance = this;
        maxEXP = 200;
        anim = GetComponent<Animator>();
        rigit = GetComponent<Rigidbody2D>();
        UI_static_present = GameObject.FindGameObjectWithTag("UI_static_present");
        UI_LevelUp = GameObject.FindGameObjectWithTag("UI_LevelUp");
        nonHurtCounter = Mathf.Infinity;
        isDead = false;
    }

    void HealChange(int healChange)
    {
        if (healChange < 0)
            currHeal = currHeal + healChange < 0 ? 0 : currHeal + healChange;
        else
            currHeal = currHeal + healChange > maxHeal ? maxHeal : currHeal + healChange;
        slider_heal.value = currHeal;
        heal_text.text = $"{currHeal}/{maxHeal}";
        if (currHeal == 0)
        {
            isDead = true;
            anim.SetTrigger("dead");
            DeathSoundEffect.Play();
        }
    }
    private void Start()
    {
    
        UI_static_present.SetActive(false);
        UI_LevelUp.SetActive(false);
    }
    private void Update()
    {
        if (maxHeal != 0 && !firstLoad) UpdateState();
        if (Input.GetKeyDown(KeyCode.F)) HealChange(10);
        if (Input.GetKeyDown(KeyCode.G)) HealChange(-50);
        if (Input.GetKeyDown(KeyCode.X)) GainEXP(20);
        if (Input.GetKey(KeyCode.T)) UI_static_present.SetActive(true);
        else UI_static_present.SetActive(false);

        nonHurtCounter += Time.deltaTime;
        // if (Input.GetKeyDown(KeyCode.Escape)) UI_In_Level_test.instance.GamePause(); 
        R.SetActive(canFlash == 1);
        M1.SetActive(canShot == 1);
    }
    /// <summary>
    /// Knight nhan vao luong sat thuong = dame. Luu y dame phai be hon 0
    /// </summary>
    /// <param name="dame"></param>
    public void TakeDame(int dame)
    {

        if (nonHurtCounter < nonHurtTimmer || isDead) return;
        HealChange(dame + strength);
        anim.SetTrigger("hit");
        HitSoundEffect.Play();
        anim.SetTrigger("non_hurt");
        rigit.AddForce(Vector2.right * 6 * -Mathf.Sign(KnightMoveset.instance.faceDir.x), ForceMode2D.Impulse);
        nonHurtCounter = 0;
    }
    /// <summary>
    /// Knight duoc hoi mot luong HP. Luu y bonusHp phai lon hon 0
    /// </summary>
    public void GainHp(int bonusHp)
    {
        HealChange(bonusHp);
        anim.SetTrigger("heal");
    }
    private void LevelUp()
    {
        currLevel++;
        currEXP = 0;
        exp_text.text = $"{currEXP}/{maxEXP}";
        level_text.text = $"Lv: {currLevel}";
        slider_exp.value = currEXP;
        anim.SetTrigger("level_up");
        LevelUpSoundEffect.Play();
        int buffHealth = Mathf.CeilToInt(maxHeal * 0.2f) > 100 ? 100 : Mathf.CeilToInt(maxHeal * 0.2f);
        maxHeal += buffHealth;
        currHeal += buffHealth;
        slider_heal.maxValue = maxHeal;
        heal_text.text = $"{currHeal}/{maxHeal}";
        if (currLevel == 5)
        {
            canFlash = 1;
            GameManager.instance.PrintMessage("You have unlocked skill Flash. Press R to try it!", 4);
        }
        if (currLevel == 7)
        {
            canShot = 1;
            GameManager.instance.PrintMessage("You have unlocked skill Shot. Right click to try it!", 6);
        }
        UI_LevelUp.SetActive(true);
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
        maxHeal = gameData.maxHp;
        currHeal = gameData.HP;
        currEXP = gameData.EXP;
        currLevel = gameData.Level;
        attackPower = gameData.attackPower;
        strength = gameData.strength;
        speed = gameData.speed;

        canFlash = gameData.canFlash;
        canShot = gameData.canShot;
        Debug.Log($"Load HP = {gameData.HP}");
        Debug.Log($"Load attack  = {gameData.attackPower}");
        Debug.Log($"Load strength = {gameData.strength}");
        Debug.Log($"Load speed = {gameData.speed}");
    }
    public void SaveData(ref GameData gameData)
    {
        gameData.maxHp = maxHeal;
        gameData.EXP = currEXP;
        gameData.Level = currLevel;
        gameData.HP = currHeal;
        gameData.speed = speed;
        gameData.attackPower = attackPower;
        gameData.strength = strength;
        gameData.canFlash = canFlash;
        gameData.canShot = canShot;
    }
    /// <summary>
    /// Is call by event in 'dead' animation
    /// </summary>
    void Dead()
    {
        //  GameManager.instance.GameOver();
        GameManager.instance.GameOverFame();
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spell")) TakeDame(-10);
    }
    void UpdateState()
    {
        slider_heal.maxValue = maxHeal;
        slider_heal.value = currHeal;
        heal_text.text = $"{currHeal}/{maxHeal}";
        slider_exp.maxValue = maxEXP;
        slider_exp.value = currEXP;
        exp_text.text = $"{currEXP}/{maxEXP}";
        level_text.text = $"Lv: {currLevel}";
        firstLoad = false;
    }
    
    
}   
