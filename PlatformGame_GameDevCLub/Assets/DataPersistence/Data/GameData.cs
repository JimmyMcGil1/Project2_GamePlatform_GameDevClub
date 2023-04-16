using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData 
{
    public int maxHp;
    public int HP;
    public int EXP;
    public int Level;
    public int attackPower;
    public int strength;
    public int speed;
    public int canFlash;
    public int canShot;
    public Vector3 playerPos;
    public GameData()
    {
        maxHp = 100;
        HP = 100;
        EXP = 0;
        Level = 1;
        attackPower = 30;
        speed = 4;
        strength = 5;
        playerPos = new Vector3(15.0f, -20.0f, 0.0f);
        canFlash = 0;
        canShot = 0;
    }
}
