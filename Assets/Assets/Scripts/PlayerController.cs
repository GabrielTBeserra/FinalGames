using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Animator _animator;
    private Rigidbody _rigidbody;
    private NavMeshAgent agent;

    private Camera cam;

    private float camInitX;
    private float camInitY;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        cam = Camera.main;
        camInitX = cam.transform.position.x;
        camInitY = cam.transform.position.y;
    }
    Vector3 lastPosition;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000f)) agent.SetDestination(hit.point);
        }


        //cam.transform.position = new Vector3(target.transform.position.x + camInitX, target.transform.position.y + camInitY, 0);


        //_animator.SetFloat("speed", 1, 0.2f, Time.deltaTime);
        //_animator.SetFloat("turn", 1, 0.2f, Time.deltaTime);

    }

    private void OnAnimatorMove()
    {
        _rigidbody.velocity = _animator.deltaPosition / Time.deltaTime;
        transform.rotation = _animator.rootRotation;
    }
}