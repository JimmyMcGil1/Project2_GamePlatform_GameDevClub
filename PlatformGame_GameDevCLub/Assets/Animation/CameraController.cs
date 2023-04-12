using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  

    Transform player;
    [SerializeField] float speed;
    [SerializeField] float xOffset;
    [SerializeField] float yOffset;
    [SerializeField] bool lockX;
    [SerializeField] bool lockY;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Knight").transform;
    }
    private void LateUpdate()
    {
        float xTarget = player.position.x + xOffset;
        float yTarget = player.position.y + yOffset;
        float xNew = (!lockX) ? Mathf.Lerp(transform.position.x, xTarget, Time.deltaTime * speed) : transform.position.x;
        float yNew = (!lockY) ? Mathf.Lerp(transform.position.y, yTarget, Time.deltaTime * speed) : transform.position.y;
        transform.position = new Vector2(xNew, yNew);
    }
    private void Update()
    {
        xOffset = (player.localScale.x > 0) ? 2 : -2;
    }
}
