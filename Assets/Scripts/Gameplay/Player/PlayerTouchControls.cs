using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerTouchControls : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;

    private Vector3 _touchPosition;
    private Vector3 _worldTouchPosition;
    private Vector3 _clampedPosition;

    private Camera _camera;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float yOffSet;

    #region Bounds
    private Vector2 _minBounds;
    private Vector2 _maxBounds;
    [Header("Paddings")]
    [SerializeField] private float paddingRight;
    [SerializeField] private float paddingLeft;
    [SerializeField] private float paddingTop;
    [SerializeField] private float paddingBottom;
    #endregion

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        _playerInputActions.Enable();
    }

    private void OnDisable()
    {
        _playerInputActions.Disable();
    }

    private void Start()
    {
        SetUpBounds();
    }

    private void FixedUpdate()
    {
        CalculatePlayerPosition();
    }

    private void CalculatePlayerPosition()
    {
        _touchPosition = _playerInputActions.Player.TouchPosition.ReadValue<Vector2>();
        _touchPosition.z = _camera.nearClipPlane;
        
        _worldTouchPosition = _camera.ScreenToWorldPoint(_touchPosition);
        _worldTouchPosition.y += yOffSet;
        
        _clampedPosition = new Vector3(Mathf.Clamp(_worldTouchPosition.x, _minBounds.x + paddingRight, _maxBounds.x - paddingLeft), 
            Mathf.Clamp(_worldTouchPosition.y, _minBounds.y + paddingBottom, _maxBounds.y - paddingTop), _worldTouchPosition.z);
        
        transform.position = Vector3.MoveTowards(transform.position, _clampedPosition, moveSpeed * Time.fixedDeltaTime);
    }
    
    private void SetUpBounds()
    {
        Camera mainCam = Camera.main;
        if (mainCam == null) return;
        _minBounds = mainCam.ViewportToWorldPoint(new Vector2(0, 0));
        _maxBounds = mainCam.ViewportToWorldPoint(new Vector2(1, 1));
    }
}
