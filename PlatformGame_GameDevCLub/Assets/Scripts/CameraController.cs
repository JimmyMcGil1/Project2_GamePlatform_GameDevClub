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
    //[SerializeField] float shake;
    //[SerializeField] float length;
    float shakeAmount;
    Vector2 veloc;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Knight").transform;
        shakeAmount = 0;
        veloc = Vector2.zero;
    }
    private void Start()
    {
        speed = player.gameObject.GetComponent<KnightStatic>().speed - 5 < 2 ? 2 : player.gameObject.GetComponent<KnightStatic>().speed - 5;
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
        //if (Input.GetKeyDown(KeyCode.Alpha1)) Shake(shake, length);
    }
    public void Shake(float amt, float length)
    {
        shakeAmount = amt;
        InvokeRepeating("DoShake", 0, 0.01f);
        Invoke("StopShake", length);
    }
    void DoShake(float amt, float lenght)
    {
        if (shakeAmount > 0 )
        {
            Vector3 camPos = gameObject.transform.position;
            float shakeAmtX = Random.value * shakeAmount * 2 - shakeAmount;
            float shakeAmtY = Random.value * shakeAmount * 2 - shakeAmount;
            camPos.x += shakeAmtX;
            camPos.y += shakeAmtY;
            gameObject.transform.position = camPos;
        }
    }
    void StopShake()
    {
        CancelInvoke("BeginShake");
        gameObject.transform.localPosition = Vector3.zero;
    }
}
