using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSimpleController : MonoBehaviour
{
    Animator animCtrl;

    void Start()
    {
        animCtrl = GetComponent<Animator>();
    }

    void Update()
    {
        float speed = Input.GetKey(KeyCode.UpArrow) ? 1f : 0f;
        animCtrl.SetFloat("speed", speed);
    }
}
