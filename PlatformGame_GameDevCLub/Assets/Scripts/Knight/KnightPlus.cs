/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightPlus : MonoBehaviour
{
    public Layer HP;
    public Layer Blood;

    private void Update()
    {
        Plus();
    }

    void Plus()
    {
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position,, HP);
        foreach(var coll in colls)
        {
            Destroy(coll.gameObject);

        }
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position,, Blood);
        foreach(var coll in colls)
        {
            Destroy(coll.gameObject);

        }
    }
}
*/