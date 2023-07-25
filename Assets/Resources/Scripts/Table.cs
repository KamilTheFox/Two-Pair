using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviour
{
    [SerializeField] private int numberAction;

    private Vector2 pointMin, pointMax;

    public Vector2 SizeTable => new Vector2(pointMax.x - pointMin.x, pointMax.y - pointMin.y);

    private Stack<int> CollectedCardID = new();

    [SerializeField] private List<Card> cards = new List<Card>();
    void Start()
    {
        RectTransform bounds = GetComponent<RectTransform>();
        pointMin = bounds.TransformPoint(bounds.rect.min);
        pointMax = bounds.TransformPoint(bounds.rect.max);
        Reset();
    }
    private void Reset()
    {
        switch (numberAction) 
        {
            case -1:
                cards.Clear();
                break;
            case 0:
                cards.Clear();
                for (int i = 0; i < 10; i++)
                {
                    cards.Add(Instantiate(Resources.Load<Card>("cards\\_Prefabs\\Card")));
                    cards.Add(Instantiate(Resources.Load<Card>("cards\\_Prefabs\\Card")));
                }
                CalculateGridCard();
                break;
            case 1:
                CalculateGridCard();
                break;
        }
        
    }
    private void CalculateGridCard()
    {
        Vector2 size = SizeTable;
        for (int i = 1; i < 5; i++)
        {
            for (int y = 1; y < 6; y++)
            {
                int indexCount = i + (y - 1) * 4 - 1;
                Vector2 sizeCard = size.x / 4 * Vector2.one;
                cards[indexCount].Size = sizeCard;
                cards[indexCount].transform.position = new Vector2(pointMin.x + sizeCard.x * (i - 1) , pointMax.y - sizeCard.x * (y - 1)) + new Vector2(sizeCard.x, -sizeCard.y);
                cards[indexCount].transform.position -= new Vector3(sizeCard.x, -sizeCard.y, 0) / 2;
            }
        }
    }
}
