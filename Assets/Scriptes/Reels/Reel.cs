using System;
using System.Collections;
using System.Linq;
using Scriptes.Audio;
using Scriptes.Const;
using Scriptes.Symbol;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scriptes.Reels
{
    public class Reel : MonoBehaviour, IReel
    {
        public event Action ReelsStopedEvent;
        
        private ISymbol[] _symbols;  
        private Vector3[] _initialPositions;

        private float _symbolHeight = 2.5f;
        private float _spinSpeed;
        private float _spinDuration;
        
        public bool IsSpinning { get; private set; }

        private void Start()
        {
            _symbols = GetComponentsInChildren<ISymbol>();  
            InitializeSymbols();  
            SaveInitialPositions();  
        }

        private void InitializeSymbols()
        {
            for (int i = 0; i < _symbols.Length; i++)
            {
                _symbols[i].GetTransform().localPosition = new Vector3(0, _symbolHeight * (9 - i), 0);
            }
        }

        public void Spin(float spinTime, int spinSpeed)
        {
            _spinSpeed = spinSpeed;
            IsSpinning = true;
            _spinDuration = spinTime;

            for (int j = 0; j < _symbols.Length; j++)
            {
                _symbols[j].GetSpriteBeground().gameObject.SetActive(false);
            }

            ShuffleSymbols();

            StartCoroutine(SpinReel());
        }

        public ISymbol[] GetVisibleSymbols()
        {
            return _symbols.OrderBy(symbol => symbol.GetTransform().localPosition.y).Take(3).ToArray();
        }

        public void Stop()
        {
            IsSpinning = false;
            AlignSymbols();
            ReelsStopedEvent?.Invoke();
            AudioManager.Instance.PlaySFX(AudioConst.ReelStop);
        }

        private void ShuffleSymbols()
        {
            for (int i = 0; i < _symbols.Length; i++)
            {
                int randomIndex = Random.Range(0, _symbols.Length);
                var temp = _symbols[i];
                _symbols[i] = _symbols[randomIndex];
                _symbols[randomIndex] = temp;
            }
            InitializeSymbols();  
        }

        private IEnumerator SpinReel()
        {
            float elapsedTime = 0f;

            while (elapsedTime < _spinDuration)
            {
                foreach (var symbol in _symbols)
                {
                    var symbolTransform = symbol.GetTransform();
                    symbolTransform.localPosition -= new Vector3(0, _spinSpeed * Time.deltaTime, 0);

                    if (symbolTransform.localPosition.y < -_symbolHeight)
                    {
                        symbolTransform.localPosition += new Vector3(0, _symbolHeight * _symbols.Length, 0);
                    }
                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            Stop();
        }

        private void SaveInitialPositions()
        {
            _initialPositions = new Vector3[_symbols.Length];
            for (int i = 0; i < _symbols.Length; i++)
            {
                _initialPositions[i] = _symbols[i].GetTransform().localPosition;
            }
        }

        private void AlignSymbols()
        {
            for (int i = 0; i < _symbols.Length; i++)
            {
                var symbolTransform = _symbols[i].GetTransform();
                symbolTransform.localPosition = _initialPositions[i];
            }
        }
    }
}
