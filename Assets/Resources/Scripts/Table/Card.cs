using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Card : MonoBehaviour, ITakeCard
{
    [SerializeField] internal byte idCard;

    private SpriteRenderer _renderer;
    public event UnityAction OnTake;
    public SpriteRenderer Renderer 
    {
        get
        {
            if(!_renderer)
                _renderer = GetComponent<SpriteRenderer>();
            return _renderer;
        }
    }
    public Sprite Sprite
    {
        set
        {
            Renderer.sprite = value;
        }
    }
    private void OnDestroy()
    {
        OnTake?.Invoke();
    }
    public Vector2 Size 
    {
        get
        {
            return Renderer.localBounds.size;
        }
        set
        {
            Bounds bounds = Renderer.bounds;
            transform.localScale = value.x / bounds.size.x * Vector3.one;
        }

    }
}
