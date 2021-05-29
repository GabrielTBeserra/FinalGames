using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YbotController2 : MonoBehaviour
{
    Animator animCtrl;
    Rigidbody rb;

    void Start()
    {
        animCtrl = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float multiplier = Input.GetKey(KeyCode.LeftShift) ? 2f : 1f;
        float vertical = Input.GetAxisRaw("Vertical") * multiplier;
        float horizontal = Input.GetAxisRaw("Horizontal");

        animCtrl.SetFloat("speed", vertical, 0.2f, Time.deltaTime);
        animCtrl.SetFloat("turn", horizontal, 0.2f, Time.deltaTime);
    }

    void OnAnimatorMove()
    {
        rb.velocity = animCtrl.deltaPosition / Time.deltaTime;
        transform.rotation = animCtrl.rootRotation;
    }
}
