using UnityEngine;

namespace Scriptes.Symbol
{
    [CreateAssetMenu(fileName = "NewSymbolData", menuName = "Symbol Data")]
    public class SymbolData : ScriptableObject
    {
        [SerializeField] private string symbolId; 
        [SerializeField] private Sprite symbolSprite;  
        [SerializeField] private Sprite symbolSpriteBeground;
        [SerializeField] private bool isWild; 
        [SerializeField] private int points; 
        public string SymbolId => symbolId;
        public Sprite SymbolSprite => symbolSprite;
        public Sprite SymbolSpriteBeground => symbolSpriteBeground;
        public bool IsWild => isWild;
        public int Point => points;
    }
}