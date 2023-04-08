using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEffect : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody2D rigit;
    Animator anim;
    private void Awake()
    {
        rigit = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
       
    }
  
}
