using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door_beheviour : MonoBehaviour
{
     Image door_slider;
    [HideInInspector] public BoxCollider2D box;
    [HideInInspector] public Rigidbody2D rigit;

    GameObject boss;
    [HideInInspector] public bool bossIsLive;
    Door_detach door_detack;
    private void Awake()
    {
        door_slider = gameObject.transform.Find("Door_canvas").Find("Door_slider").gameObject.GetComponent<Image>();
        boss = GameObject.FindGameObjectWithTag("BOSS");
        box = GetComponent<BoxCollider2D>();
     //   rigit = GetComponent<Rigidbody2D>();
        door_detack = gameObject.transform.Find("Door_Detack").gameObject.GetComponent<Door_detach>();
        bossIsLive = false;
    }
    private void Start()
    {
        boss.SetActive(false);
    }
    public IEnumerator Unlock()
    {
        Debug.Log("door unlock");
        for (int i = 0; i < 20; i++)
        {
            door_slider.fillAmount -= 1/20f;
            yield return new WaitForSeconds(1/ 20f);
        }
        box.isTrigger = true;
        bossIsLive = true;
        GameManager.instance.PrintMessage("Lucky for you to meet Dark Lord. Be elegant with him but I'm not sure he do", 7f);
        boss.SetActive(true);
        StartCoroutine(boss.transform.Find("Boss1").gameObject.GetComponent<boss1_beheviour>().StartRun()); 
        StartCoroutine(boss.transform.Find("Boss1").gameObject.GetComponent<boss1_beheviour>().StartSummon()); 
    }
    public IEnumerator Lock()
    {
        for (int i = 0; i < 20; i++)
        {
            door_slider.fillAmount += 1/20f;
            yield return new WaitForSeconds(1/20f);
        }
        box.isTrigger = false;
    }
    public void BossDie()
    {
        StartCoroutine(BossDieUnlock());
    }
    IEnumerator BossDieUnlock()
    {
        for (int i = 0; i < 10; i++)
        {
            door_slider.fillAmount -= 1 / 10f;
            yield return new WaitForSeconds(1 / 10f);
        }
        gameObject.SetActive(false);
    }
}
