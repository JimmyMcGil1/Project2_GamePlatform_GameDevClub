using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door_beheviour : MonoBehaviour
{
     Image door_slider;
    public BoxCollider2D box;
    public Rigidbody2D rigit;
    Door_detach door_detack;
    private void Awake()
    {
        door_slider = gameObject.transform.Find("Door_canvas").Find("Door_slider").gameObject.GetComponent<Image>();
        box = GetComponent<BoxCollider2D>();
        rigit = GetComponent<Rigidbody2D>();
        door_detack = gameObject.transform.Find("Door_detach").gameObject.GetComponent<Door_detach>();
        
    }
    private void Update()
    {
       
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
        GameManager.instance.PrintMessage("Lucky for you to meet Dark Lord. Be elegant with him but I'm not sure he do", 7f);
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
}
