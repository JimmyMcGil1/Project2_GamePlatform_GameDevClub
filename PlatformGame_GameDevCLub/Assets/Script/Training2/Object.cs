using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public float V;
    public GameObject dan;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Shot();
    }
    private void OnMouseDrag()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        transform.position = mousePos;
    }

    void Shot()
    {
        Instantiate(dan, transform.position, Quaternion.identity);
    }    
}
