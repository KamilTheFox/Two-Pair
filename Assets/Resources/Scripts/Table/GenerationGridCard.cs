using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = System.Random;

[SerializeField]
internal class GenerationGridCard : IDisposable
{
    [SerializeField] private List<Card> cards = new List<Card>();

    [SerializeField] private Vector2 SizeGrid;

    [SerializeField] private GameObject ArrayCard;

    private List<Sprite> Sprites = new();

    private List<GridCell> Grid = new();

    private Random random = new Random();

    private struct GridCell
    {
        public GridCell(Vector2 size, Vector2 position)
        {
            this.size = size;
            this.position = position;
        }
        public Vector2 size, position;
    }

    internal GenerationGridCard(Vector2 size)
    {
        SizeGrid = size;

        ArrayCard = new GameObject("ArrayCard");
        List<Game_Level.CardInfo> value;
        if(!Configuration.GetValue($"level_{Game_Level.GetCurrentLevel}","Cards",out value))
        {
            value = new List<Game_Level.CardInfo>() { new(TypeCard.balls, 20) };
        }

        List<byte> vs = new();
        byte x = 0; 
        for(int i = 0; i< value.Count; i++)
            foreach (Sprite sprite in Resources.LoadAll<Sprite>("cards\\" + value[i].TypeCard))
            {
                Sprites.Add(sprite);
                vs.Add(x);
                x++;
            }
        for (int i = 0; i < value.Sum((t) => t.Count); i++)
        {
            byte id = (byte)random.Next(0, vs.Count);
            byte idCard = vs[id];
            vs.Remove(idCard);
            Card card = Resources.Load<Card>("cards\\_Prefabs\\Card");
            card.idCard = idCard;
            card.Sprite = Sprites[idCard];
            Card card1 = GameObject.Instantiate(card);
                Card card2 = GameObject.Instantiate(card);
            card1.OnTake += CurrentGenerationCard;
            card2.OnTake += CurrentGenerationCard;
            cards.Add(card1);
            cards.Add(card2);
        }
        foreach (Card card in cards)
        {
            card.transform.SetParent(ArrayCard.transform);
            card.gameObject.SetActive(false);
        }
        CalculateGrid();
        ArrangeCells();
    }
    public Vector2 AmendmentVector
    {
        set
        {
            ArrayCard.transform.position = value;
        }
    }
    public void Dispose()
    {
        throw new NotImplementedException();
    }
    private void ArrangeCells()
    {
        for (int r = 0; r < cards.Count; r++)
        {
            Card card = cards[r];
            int rnd = random.Next(0, cards.Count);
            cards[r] = cards[rnd];
            cards[rnd] = card;
        }
        for (int i = 0; i < 20; i++)
        {
            if (cards.Count <= i) break;
            cards[i].transform.position = Grid[i].position;
            cards[i].Size = Grid[i].size;
            cards[i].gameObject.SetActive(true);
            CurrentGener = i;
        }
    }
    private int CurrentGener;
    private void CurrentGenerationCard()
    {
        if (cards.Count <= CurrentGener) return;
        cards[CurrentGener].transform.position = Grid[CurrentGener].position;
        cards[CurrentGener].Size = Grid[CurrentGener].size;
        cards[CurrentGener].gameObject.SetActive(true);
        CurrentGener++;
    }
    private void CalculateGrid()
    {
        Vector2 size = SizeGrid;
        for (int sizer = 0; sizer < cards.Count / 20 + 1; sizer++)
        for (int i = 1; i < 5; i++)
        {
            for (int y = 1; y < 6; y++)
            {
                int indexCount = i + (y - 1) * 4 - 1;
                Vector2 sizeCard = size.x / 4 * Vector2.one;
                Vector2 positon = new Vector2(sizeCard.x * (i - 1), -sizeCard.x * (y - 1)) + new Vector2(sizeCard.x, -sizeCard.y);
                positon -= new Vector2(sizeCard.x, -sizeCard.y) / 2;
                Grid.Add(new(sizeCard, positon));
            }
        }
    }
}
