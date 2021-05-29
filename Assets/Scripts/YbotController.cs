using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YbotController : MonoBehaviour
{
    [SerializeField]
    private float angSpeed = 30f;
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
        if(vertical != 0)
            transform.Rotate(0f, horizontal * angSpeed * Time.deltaTime, 0f);

        animCtrl.SetFloat("speed", vertical, 0.2f, Time.deltaTime);
    }
}
