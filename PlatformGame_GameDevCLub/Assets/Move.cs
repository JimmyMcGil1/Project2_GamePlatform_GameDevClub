using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position; 
        if (Input.GetKeyDown(KeyCode.D))
            pos.x += Time.deltaTime * speed;
        if (Input.GetKeyDown(KeyCode.A))
            pos.x -= Time.deltaTime * speed;
    }
}
