using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolScript : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Transform currentPoint;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        if (currentPoint == pointA.transform)
        {
            rb.velocity = new Vector2(-speed, 0);
        }
        if ((Vector2.Distance(transform.position, currentPoint.position) < 0.5f) && currentPoint == pointB.transform)
        {
            currentPoint = pointA.transform;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if ((Vector2.Distance(transform.position, currentPoint.position) < 0.5f) && currentPoint == pointA.transform)
        {
            currentPoint = pointB.transform;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}

