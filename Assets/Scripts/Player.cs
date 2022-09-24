using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Vector2 rawInput;

    [SerializeField] private float moveSpeed;
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
        Debug.Log(rawInput);
    }

    private void Move()
    {
        Vector3 delta = rawInput;
        transform.position += delta * moveSpeed * Time.deltaTime;
    }
}
