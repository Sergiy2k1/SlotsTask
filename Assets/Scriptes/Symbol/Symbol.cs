using UnityEngine;

namespace Scriptes.Symbol
{
    public class Symbol : MonoBehaviour, ISymbol
    {
        [SerializeField] private SymbolData symbolData;  

        private string _symbolId;
        private bool _isWild;
        private int _points; 

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private SpriteRenderer spriteBeground;
        
        public string GetId() => _symbolId;
        public bool IsWild() => _isWild;
        public Transform GetTransform() => transform;
        public SpriteRenderer GetSpriteRenderer() => spriteRenderer;
        public SpriteRenderer GetSpriteBeground() => spriteBeground;
        public int GetPoints() => _points;

        private void OnEnable()
        {
            InitializeFromData();
        }


        private void InitializeFromData()
        {
            if (symbolData != null)
            {
                _symbolId = symbolData.SymbolId;
                _isWild = symbolData.IsWild;
                _points = symbolData.Point;

                if (spriteRenderer != null)
                {
                    spriteRenderer.sprite = symbolData.SymbolSprite;
                }

                if (spriteBeground != null)
                {
                    spriteBeground.sprite = symbolData.SymbolSpriteBeground;
                    spriteBeground.gameObject.SetActive(false);
                }
            }
            else
            {
                Debug.LogError("SymbolData is not assigned!");
            }
        }
    }
}