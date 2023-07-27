using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviour
{
    public static Table Instance { get; private set; }

    private Vector2 pointMin, pointMax, pointMinBack, pointMaxBack;
    public Vector2 SizeBackground => new Vector2(pointMaxBack.x - pointMinBack.x, pointMaxBack.y - pointMinBack.y);
    public Vector2 SizeTable => new Vector2(pointMax.x - pointMin.x, pointMax.y - pointMin.y);

    [SerializeField] internal GenerationGridCard generator;
    void Start()
    {
        Instance = this;
        RectTransform bounds = GetComponent<RectTransform>();
        pointMin = bounds.TransformPoint(bounds.rect.min);
        pointMax = bounds.TransformPoint(bounds.rect.max);
        RectTransform boundsBack = transform.parent.gameObject.GetComponent<RectTransform>();
        pointMinBack = bounds.TransformPoint(boundsBack.rect.min);
        pointMaxBack = bounds.TransformPoint(boundsBack.rect.max);
        generator = new(SizeTable);
        generator.AmendmentVector = new Vector2(pointMin.x, pointMax.y);
    }
    
}
