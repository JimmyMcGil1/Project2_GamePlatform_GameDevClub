using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plus : MonoBehaviour
{
    [SerializeField] int bonusHp;
    [SerializeField] int bonusExp;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Knight"))
        {
            if (bonusHp > 0) KnightStatic.instance.GainHp(bonusHp);
            else KnightStatic.instance.GainEXP(bonusExp);
            Destroy(gameObject);
        }
    }
}   
