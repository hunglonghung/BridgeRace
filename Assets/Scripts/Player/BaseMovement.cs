using UnityEngine;

public class BaseMovement : MonoBehaviour
{
    public enum MovementState
    {
        Flat,
        Slope,
        Idle,
        Win
    }
    [Header("Movement Basics")]
    public float MoveSpeed;
    public Rigidbody rb;
    public Animator anim;
    public string currentAnimName;
    [Header("Slope Handling")]
    [SerializeField] public float maxSlopeAngle;
    [SerializeField] public float directionMagnitute ;
    private RaycastHit slopeHit;
    [SerializeField] float gravityForce;
    [Header("Movement")]
    public MovementState movementState = MovementState.Flat;
    public Vector3 MoveDirection;
    [SerializeField] public bool isWin;

    // Định nghĩa các phương thức di chuyển chung
    protected void Move(Vector3 direction)
    {
        rb.velocity = direction * MoveSpeed;
        rb.rotation = Quaternion.LookRotation(direction);
        ChangeAnim("running");
    }

    protected void Idle()
    {
        ChangeAnim("idle");
        rb.velocity = Vector3.zero;
    }

    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(currentAnimName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }
        //Slope Movement

    public void MoveOnSlope(Vector3 direction)
    {
        if (OnSlope())
        {
            rb.AddForce(GetSlopeMoveDirection() * MoveSpeed * 20f, ForceMode.Force);
            ChangeAnim("running");
            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * gravityForce, ForceMode.Force);
        }
        rb.rotation = Quaternion.LookRotation(direction);
        
        // Vector3 slopeDirection = GetSlopeMoveDirection();
        // float gravity = 9.81f;  
        // Vector3 gravityForce = Vector3.down * gravity * Time.fixedDeltaTime;
        // float slopeSpeedAdjustment = 1.5f; 
        // rb.velocity = (slopeDirection * MoveSpeed * slopeSpeedAdjustment) + gravityForce;
    }
    public bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, 10 * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            // Debug.Log(angle < maxSlopeAngle && angle != 0);
            return angle < maxSlopeAngle && angle != 0;
            
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(MoveDirection, slopeHit.normal).normalized;
    }
}
