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
        string value = "balls";
        Configuration.GetValue("", "", out short value1);
        Configuration.GetValue("", "", out ushort value2);
        Configuration.GetValue("", "", out byte value3);
        Configuration.GetValue("", "", out sbyte value4);
        Configuration.GetValue("", "", out bool value5);
        Configuration.GetValue("", "", out ComboStar.StateCombo value6);
        //string value = Configuration.Instance[$"level_{Game_Level.GetCurrentLevel}\\SpriteName"];
        if (value == null)
        {
            value = "balls";
            //Configuration.Instance[$"level_{Game_Level.GetCurrentLevel}\\SpriteName"] = value;
        }
        List<byte> vs = new();
        byte x = 0; 
        foreach (Sprite sprite in Resources.LoadAll<Sprite>("cards\\" + value))
        {
            Sprites.Add(sprite);
            vs.Add(x);
            x++;
        }
            
        for (int i = 0; i < 10; i++)
        {
            byte id = (byte)random.Next(0, vs.Count);
            byte idCard = vs[id];
            vs.Remove(idCard);
            Card card = Resources.Load<Card>("cards\\_Prefabs\\Card");
            card.idCard = idCard;
            card.Sprite = Sprites[idCard];
            cards.Add(GameObject.Instantiate(card));
            cards.Add(GameObject.Instantiate(card));
            
        }
        foreach (Card card in cards)
        {
            card.transform.SetParent(ArrayCard.transform);
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
        foreach(GridCell cell in Grid)
        {
            int rnd = random.Next(0, cards.Count);
            cards[rnd].transform.position = cell.position;
            cards[rnd].Size = cell.size;
            cards.RemoveAt(rnd);
        }
    }
    private void CalculateGrid()
    {
        Vector2 size = SizeGrid;
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
