using UnityEngine;

public class Game_Level : MonoBehaviour
{
    public static int GetCurrentLevel => 1;
    private void Start()
    {
        
    }
    public struct CardInfo
    {
        public CardInfo(TypeCard type, int count)
        {
            Count = count;
            TypeCard = type;
        }
        public int Count { get; private set; }
        public TypeCard TypeCard { get; private set; }
    }
}
