using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Vector2 _rawInput;

    private Vector2 _minBounds;
    private Vector2 _maxBounds;

    [SerializeField] private float moveSpeed;

    private Shooter _shooter;
    private bool _shooterInitialized;

    public event Action<bool> OnFiringEvent;

    #region Paddings
    [Header("Paddings")]
    [SerializeField] private float paddingRight;
    [SerializeField] private float paddingLeft;
    [SerializeField] private float paddingTop;
    [SerializeField] private float paddingBottom;
    #endregion

    private void Awake()
    {
        InitializeShooter();
    }

    private void InitializeShooter()
    {
        _shooter = GetComponent<Shooter>();
        _shooterInitialized = true;
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
        transform.position = CalculateNewPosition(CalculateDelta());
    }

    private Vector2 CalculateDelta()
    {
        return _rawInput * (moveSpeed * Time.deltaTime);
    }

    private Vector2 CalculateNewPosition(Vector2 delta)
    {
        var position = transform.position;
        var headedTowardsX = position.x + delta.x;
        var headedTowardsY = position.y + delta.y;

        return new Vector2(Mathf.Clamp(headedTowardsX, _minBounds.x + paddingRight, _maxBounds.x - paddingLeft), 
            Mathf.Clamp(headedTowardsY, _minBounds.y + paddingBottom, _maxBounds.y - paddingTop));
    }

    void OnFire(InputValue value)
    {
        if (_shooterInitialized)
        {
            OnFiringEvent?.Invoke(value.isPressed);
        }
    }
    
}
