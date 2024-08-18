using UnityEngine;

namespace Scriptes.Symbol
{
    public class Symbol : MonoBehaviour, ISymbol
    {
        [SerializeField] private SymbolData symbolData;  

        private string symbolId;
        private bool isWild;
        private int points; // Додано поле для кількості очок

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private SpriteRenderer spriteBeground;

        private void Awake()
        {
            if (spriteRenderer == null)
            {
                Debug.LogError("SpriteRenderer component is missing on this GameObject!");
            }
        }

        private void OnEnable()
        {
            InitializeFromData();
        }

        private void InitializeFromData()
        {
            if (symbolData != null)
            {
                symbolId = symbolData.SymbolId;
                isWild = symbolData.IsWild;
                points = symbolData.Point;

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

        public string GetId() => symbolId;
        public bool IsWild() => isWild;
        public Transform GetTransform() => transform;
        public SpriteRenderer GetSpriteRenderer() => spriteRenderer;
        public SpriteRenderer GetSpriteBeground() => spriteBeground;
        public int GetPoints() => points; // Метод для отримання очок
    }
}