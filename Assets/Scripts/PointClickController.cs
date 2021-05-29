using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PointClickController : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private Camera cam;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        cam = Camera.main;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 1000f))
            {
                agent.SetDestination(hit.point);
                Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.red, 5f);
            }
        }
    }
}
