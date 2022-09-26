using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Vector2 _rawInput;
    private Vector2 _delta;
    private Vector2 _newPos;

    private Vector2 _minBounds;
    private Vector2 _maxBounds;

    [SerializeField] private float moveSpeed;

    private Shooter _shooter;

    #region Paddings
    [Header("Paddings")]
    [SerializeField] private float paddingRight;
    [SerializeField] private float paddingLeft;
    [SerializeField] private float paddingTop;
    [SerializeField] private float paddingBottom;
    #endregion

    private void Awake()
    {
        _shooter = GetComponent<Shooter>();
    }

    private void Start()
    {
        SetUpBounds();
    }

    void Update()
    {
        Move();
    }
   
    private void SetUpBounds()
    {
        Camera mainCam = Camera.main;
        if (mainCam == null) return;
        _minBounds = mainCam.ViewportToWorldPoint(new Vector2(0, 0));
        _maxBounds = mainCam.ViewportToWorldPoint(new Vector2(1, 1));
    }

    void OnMove(InputValue value)
    {
        _rawInput = value.Get<Vector2>();
    }

    

    private void Move()
    {
        _delta = _rawInput * (moveSpeed * Time.deltaTime);
        SetNewPos();
        transform.position = _newPos;

    }

    private void SetNewPos()
    {
        var position = transform.position;
        var headedTowardsX = position.x + _delta.x;
        var headedTowardsY = position.y + _delta.y;


        _newPos.x = Mathf.Clamp(headedTowardsX, _minBounds.x + paddingRight, _maxBounds.x - paddingLeft);
        _newPos.y = Mathf.Clamp(headedTowardsY, _minBounds.y + paddingBottom, _maxBounds.y - paddingTop);
    }

    void OnFire(InputValue value)
    {
        if (_shooter != null)
        {
            _shooter.isFiring = value.isPressed;
        }
    }
    
}
