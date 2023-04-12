using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plus : KnightStatic
{
    public LayerMask HP;
    public LayerMask Blood;
    public float l;
    
    void plusHP()
    {
        Collider2D[] HPs = Physics2D.OverlapCircleAll(transform.position, l, HP);
        foreach(var HP in HPs)
        {
            KnightStatic.instance.GainHp(10);
            Destroy(HP.gameObject);
        } 
    }

    void plusEXP()
    {
        Collider2D[] Bloods = Physics2D.OverlapCircleAll(transform.position, l, Blood);
        foreach(var Blood in Bloods)
        {
            KnightStatic.instance.GainEXP(5);
            Destroy(Blood.gameObject);
        } 
    }
}   
