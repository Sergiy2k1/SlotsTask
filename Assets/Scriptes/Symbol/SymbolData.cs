using UnityEngine;

namespace Scriptes.Symbol
{
    [CreateAssetMenu(fileName = "NewSymbolData", menuName = "Symbol Data")]
    public class SymbolData : ScriptableObject
    {
        [SerializeField] private string symbolId;  // Назва символу
        [SerializeField] private Sprite symbolSprite;  // Спрайт символу
        [SerializeField] private Sprite symbolSpriteBeground;
        [SerializeField] private bool isWild;  // Чи є символ "Wild"
        [SerializeField] private int points; // Додано поле для кількості очок
        public string SymbolId => symbolId;
        public Sprite SymbolSprite => symbolSprite;
        public Sprite SymbolSpriteBeground => symbolSpriteBeground;
        public bool IsWild => isWild;
        public int Point => points;
    }
}