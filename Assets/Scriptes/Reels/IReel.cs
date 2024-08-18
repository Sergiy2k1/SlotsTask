using Scriptes.Symbol;

namespace Scriptes.Reels
{
    public interface IReel
    {
        void Spin(float spinTime,int spinSpeed); 
        ISymbol[] GetVisibleSymbols();  
        void Stop();  
        bool IsSpinning { get; } 
    }
}
