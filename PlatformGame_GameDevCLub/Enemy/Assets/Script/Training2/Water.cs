using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    //public float acsimetForce;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("FloatObject")) //so sánh tag của object tương tác với tag "FloatObject"
        {
            Object obj = collision.GetComponent<Object>();
            float VObject = obj.V;
            Rigidbody2D objectRigi = collision.GetComponent<Rigidbody2D>(); // gán vật lí của object tương tác vào objectRigi
            objectRigi.AddForce(new Vector2(0, 10*VObject)); //Cung cấp một lực đẩy vào objectRigi.
        }
    }
}
