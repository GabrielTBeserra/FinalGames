using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform target;
    
    private Animator _animator;
    private Rigidbody _rigidbody;
    private NavMeshAgent _agent;
    private Camera _camera;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000f)) _agent.SetDestination(hit.point);
        }


        //_animator.SetFloat("speed", 1, 0.2f, Time.deltaTime);
        //_animator.SetFloat("turn", 1, 0.2f, Time.deltaTime);
    }

    private void OnAnimatorMove()
    {
        _rigidbody.velocity = _animator.deltaPosition / Time.deltaTime;
        transform.rotation = _animator.rootRotation;
    }
}