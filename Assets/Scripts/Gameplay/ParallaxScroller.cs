using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class ParallaxScroller : MonoBehaviour
{

    [SerializeField] Vector2 moveSpeed;

    private Vector2 _offset;

    private Material _material;
    
    void Awake()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {
        _offset = moveSpeed * Time.deltaTime;
        _material.mainTextureOffset += _offset;
    }
}
