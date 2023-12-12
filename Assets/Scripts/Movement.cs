using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private Vector2 m_Velocity = Vector2.zero; //movement smoothing thing
    private const float m_MovementSmoothing = 0.05f; //movement smoothing rate

    protected Vector2 _inputDirection;
    public float MovementSpeed = 10f; //movement speed

    protected WeaponHand _weaponHandler;
    protected bool _isMoving = false;

    protected Vector2 _targetVelocity = Vector2.zero; // for aiming controls

    Rigidbody2D _rigidBody; //player body
    Collider2D _collider2D; //player collider

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
        _weaponHandler = GetComponent<WeaponHand>();

    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement(); //movement controls
        HandleInput(); //key input controls
        HandleRotation(); //rotation (aiming) controls
    }
    protected virtual void HandleRotation()
    {
        if (_inputDirection == Vector2.zero)
            return;

        transform.rotation = Quaternion.LookRotation(Vector3.forward, (_targetVelocity)); //movement based on wsad
    }

    protected virtual void HandleInput()
    {
    }
    protected virtual void HandleMovement()
    {
        if (_rigidBody == null || _collider2D == null)
            return;

        Vector2 targetVelocity = Vector2.zero;

        targetVelocity = new Vector2(_inputDirection.x * MovementSpeed, _inputDirection.y * MovementSpeed); //movement direction

        _rigidBody.velocity = Vector2.SmoothDamp(_rigidBody.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing); //decrease speed

        _isMoving = targetVelocity.x != 0 || targetVelocity.y != 0;

        _targetVelocity = targetVelocity;
    }
}
