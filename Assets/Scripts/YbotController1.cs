using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YbotController1 : MonoBehaviour
{
    Animator animCtrl;

    void Start()
    {
        animCtrl = GetComponent<Animator>();
    }

    void Update()
    {
        float multiplier = Input.GetKey(KeyCode.LeftShift) ? 2f : 1f; 
        float vertical = Input.GetAxisRaw("Vertical") * multiplier;
        float horizontal = Input.GetAxisRaw("Horizontal");

        animCtrl.SetFloat("speed", vertical, 0.2f, Time.deltaTime);
        animCtrl.SetFloat("turn", horizontal, 0.2f, Time.deltaTime);
    }
}
