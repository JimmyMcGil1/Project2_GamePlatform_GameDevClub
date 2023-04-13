using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Print_Text : MonoBehaviour
{
    // Start is called before the first frame update
    public  Text dialogueText;
    public  string message;
    public  float letterSpeed;
    public static Print_Text instance { get; set; }
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
    }
    public void PrintMessage(string msg)
    {
      //  gameObject.SetActive(true);
        message = msg;
         StartCoroutine(Typing());
    }
    
     IEnumerator Typing()
    {
        dialogueText.text = "";
        foreach (var letter in message.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(letterSpeed);
        }
        for (int i = 0; i < 1; i++)
        {
            yield return new WaitForSeconds(4);
        }
      //  gameObject.SetActive(false);
    }
}
