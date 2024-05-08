using System.Collections;
using System.Collections.Generic;
using Scriptable;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCharacter : MonoBehaviour
{
    [Header("State")]
    [SerializeField] private IState<EnemyCharacter> currentState;
    [Header("Movement")]
    public float MoveSpeed;
    public Rigidbody rb;
    public Animator anim;
    public string currentAnimName;
    public Vector3 MoveDirection;
    [Header("Slope Handling")]
    [SerializeField] public float maxSlopeAngle;
    [SerializeField] public float directionMagnitute ;
    private RaycastHit slopeHit;
    [SerializeField] float gravityForce;
    [SerializeField] bool isWin;
    [Header("Enemy AI")]
    [SerializeField] NavMeshAgent agent;
    [Header("Block control")]
    [SerializeField] float sphereRadius;
    [SerializeField] public List<GameObject> targetObjects;
    [SerializeField] GameObject target;
    [SerializeField] public int blockCount;
    [SerializeField] public int blockIndex = 0;
    [SerializeField] Material material;
    [Header("Bridge control")]
    [SerializeField] public List<GameObject> targetUnblockObjects;
    [SerializeField] public int bridgeIndex = 0;
    private void Start()
    {
        ChangeState(new IdleState());
    }

    // Update is called once per frame
    void Update()
    {
        if (isWin || currentState == null) return;
        {
            Debug.Log(currentState);
            currentState.OnExecute(this);
        }
    }
    //Change State

    public void ChangeState(IState<EnemyCharacter> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    // Collect block
    public void SetTarget()
    {
        Vector3 center = transform.position; 
        float radius = 30f; 
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        targetObjects = new List<GameObject>(); 

        foreach (var hitCollider in hitColliders)
        {
            Debug.Log(hitCollider.gameObject.name);
            Debug.Log(hitCollider.gameObject.tag);
            Color hitColliderColor = hitCollider.GetComponent<MeshRenderer>().material.color;
            if(hitCollider.gameObject.CompareTag("Block") && material.color == hitColliderColor)
            {
                Debug.Log(hitColliderColor);
                targetObjects.Add(hitCollider.gameObject); 
            }
            if(hitCollider.gameObject.CompareTag("Unblock"))
            {
                targetUnblockObjects.Add(hitCollider.gameObject);
            }
        }

    }
    public void MoveToTarget(List<GameObject> targets)
    {
        blockCount = GetComponent<BrickControl>().blockCount;
        if (targets.Count == 0) return;
        if(blockIndex < targets.Count)
        {
            GameObject target = targets[blockIndex]; 
            float speed = 5f; 
            float step = speed * Time.deltaTime; 
            agent.SetDestination(target.transform.position);
            Debug.Log(agent.remainingDistance);
            Debug.DrawRay(transform.position,transform.position - Vector3.down * 10f);
            Debug.DrawRay(target.transform.position,target.transform.position - Vector3.down * 10f);
            if(agent.remainingDistance < 1.5f)
            {
                blockIndex ++;
            }
            
            if(blockCount == Random.Range(3,6))
            {
                ChangeState(new BuildState());
            }
        }
        else ChangeState(new IdleState());
        
    }
    // Build brick
    public void SetBridgeTarget()
    {
        Vector3 center = transform.position; 
        float radius = 30f; 
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        targetUnblockObjects = new List<GameObject>(); 

        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.gameObject.CompareTag("Unblock"))
            {
                targetUnblockObjects.Add(hitCollider.gameObject);
            }
        }
    }
    public void BridgeBuild(List<GameObject> targets)
    {
        blockCount = GetComponent<BrickControl>().blockCount;
        if(blockCount == 0)
        {
            ChangeState(new CollectState());
        }
        if(bridgeIndex < targets.Count)
        {
            GameObject target = targets[bridgeIndex]; 
            float speed = 5f; 
            float step = speed * Time.deltaTime; 
            agent.SetDestination(target.transform.position);
            bridgeIndex ++;
        }
    }
    // Win
    private void Win()
    {
        ChangeAnim("dancing");
        rb.velocity = Vector3.zero;
        isWin = true;
    }
    //Animation
    public void ChangeAnim(string animName)
    {
        if(currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    

}
