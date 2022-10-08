using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2 _rawInput;

    private Vector2 _minBounds;
    private Vector2 _maxBounds;

    [SerializeField] private float moveSpeed;

    #region Paddings
    [Header("Paddings")]
    [SerializeField] private float paddingRight;
    [SerializeField] private float paddingLeft;
    [SerializeField] private float paddingTop;
    [SerializeField] private float paddingBottom;
    #endregion

    private PlayerInputActions _playerInputActions;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
    }

    private void Start()
    {
        SetUpBounds();
        _playerInputActions.Player.Enable();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        var playerInput = _playerInputActions.Player.Move.ReadValue<Vector2>();
        transform.position = CalculateNewPosition(CalculateDelta(playerInput));
    }

   
    private void SetUpBounds()
    {
        Camera mainCam = Camera.main;
        if (mainCam == null) return;
        _minBounds = mainCam.ViewportToWorldPoint(new Vector2(0, 0));
        _maxBounds = mainCam.ViewportToWorldPoint(new Vector2(1, 1));
    }

    private Vector2 CalculateDelta(Vector2 input)
    {
        return input * (moveSpeed * Time.deltaTime);
    }

    private Vector2 CalculateNewPosition(Vector2 delta)
    {
        var position = transform.position;
        var headedTowardsX = position.x + delta.x;
        var headedTowardsY = position.y + delta.y;

        return new Vector2(Mathf.Clamp(headedTowardsX, _minBounds.x + paddingRight, _maxBounds.x - paddingLeft), 
            Mathf.Clamp(headedTowardsY, _minBounds.y + paddingBottom, _maxBounds.y - paddingTop));
    }
}
