using UnityEngine;

namespace Scriptes.Symbol
{
    public interface ISymbol
    {
        string GetId();
        SpriteRenderer GetSpriteRenderer();
        SpriteRenderer GetSpriteBeground();
        Transform GetTransform();
        bool IsWild();
        int GetPoints();
    }
}