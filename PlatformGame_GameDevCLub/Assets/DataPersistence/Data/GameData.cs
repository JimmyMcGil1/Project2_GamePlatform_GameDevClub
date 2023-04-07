using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData 
{
    public int HP;
    public int EXP;
    public int Level;
    public Vector3 playerPos;
    public GameData()
    {
        HP = 100;
        EXP = 0;
        Level = 1;
        playerPos = Vector3.zero;
    }
}
