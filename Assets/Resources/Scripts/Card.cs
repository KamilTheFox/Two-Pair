using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private SpriteRenderer Renderer 
    {
        get
        {
            if(!_renderer)
                _renderer = GetComponent<SpriteRenderer>();
            return _renderer;
        }
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
