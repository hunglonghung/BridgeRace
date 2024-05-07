using System.Collections;
using System.Collections.Generic;
using Scriptable;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCharacter : MonoBehaviour
{
    [Header ("Player")]
    [SerializeField] public GameObject player;
    private IState<EnemyCharacter> currentState;
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
    [SerializeField] Camera cam;
    [SerializeField] NavMeshAgent agent;
    [Header("Block control")]
    [SerializeField] float sphereRadius;
    [SerializeField] Collider[] hitColliders;
    [SerializeField] GameObject target;
    [SerializeField] public int blockCount;

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
    public void StartMoving()
    {
        if(player.GetComponent<PlayerMovement>().hasMoved == true)
        {
            ChangeState(new CollectState());
        }
    }
    public void SetTarget()
    {
        Vector3 center = transform.position; 
        float radius = 5.0f; 
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("block"))
            {
                target = hitCollider.gameObject;
            }
        }
    }
    public void MoveToTarget()
    {
        Vector3 curPos = transform.position;
        transform.position = Vector3.MoveTowards(curPos,target.transform.position,10f);
    }
    // Build brick
    public void BridgeBuild()
    {
        blockCount = GetComponent<BrickControl>().blockCount;
        if(blockCount == Random.Range(3,6))
        {
            ChangeState(new BuildState());
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
