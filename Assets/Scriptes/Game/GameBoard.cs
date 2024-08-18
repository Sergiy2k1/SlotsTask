using System.Collections;
using System.Linq;
using Scriptes.Reels;
using Scriptes.Symbol;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scriptes.Game
{
    public class GameBoard : MonoBehaviour
    {
        [SerializeField] private int spinSpeed;
        [SerializeField] private float spinDuration;
        [SerializeField] private float trailSpeed = 5.0f;
        [SerializeField] private float spinDelay;
        
        [SerializeField] private Reel[] reels;
        [SerializeField] private GameObject[] trails;
        [SerializeField] private Button spinButton;
        [SerializeField] private TMP_Text score; 

        private int stoppedReelsCount = 0;

        private void Start()
        {
            SubscribeEvents();
        }

        private void OnDestroy()
        {
            UnSubscribeEvents();
        }

        public void SpinReels()
        {
            stoppedReelsCount = 0;
            spinButton.interactable = false; 
            
            ResetTrails();

            for (int i = 0; i < reels.Length; i++)
            {
                float delay = i * spinDelay;
                StartCoroutine(StartReelWithDelay(reels[i], spinDuration, delay));
            }
        }

        public void ReelStopped()
        {
            stoppedReelsCount++;

            if (stoppedReelsCount == reels.Length)
            {
                DisplayWinningLines();
                spinButton.interactable = true; 
            }
        }

        public void DisplayWinningLines()
        {
            int totalScore = 0; 

            for (int i = 0; i < 3; i++) 
            {
                var symbols = reels.Select(reel => reel?.GetVisibleSymbols()?[i]).ToArray();

                if (symbols.Contains(null))
                {
                    Debug.LogError("One or more symbols are null. Check Reel initialization.");
                    continue;
                }
                
                Debug.Log($"Row {i + 1}: Symbols: {string.Join(", ", symbols.Select(s => s.GetId()))}");

                int startIndex, endIndex, lineScore;
                if (IsWinningLine(symbols, out startIndex, out endIndex, out lineScore))
                {
                    Debug.Log($"Winning line at row {i + 1} with symbols: {string.Join(", ", symbols.Skip(startIndex).Take(endIndex - startIndex + 1).Select(s => s.GetId()))}");
                    
                    totalScore += lineScore;
                    
                    StartCoroutine(MoveTrail(trails[i], symbols.Skip(startIndex).Take(endIndex - startIndex + 1).ToArray()));
                }
            }
            
            UpdateScore(totalScore);
        }

        private bool IsWinningLine(ISymbol[] symbols, out int startIndex, out int endIndex, out int totalPoints)
        {
            startIndex = -1;
            endIndex = -1;
            totalPoints = 0;

            if (symbols.Length < 3)
                return false;

            string baseSymbol = null;
            int comboLength = 0;

            for (int i = 0; i < symbols.Length; i++)
            {
                if (symbols[i].IsWild())
                {
                    if (comboLength == 0)
                    {
                        startIndex = i;
                    }
                    comboLength++;
                }
                else if (baseSymbol == null || symbols[i].GetId() == baseSymbol)
                {
                    if (baseSymbol == null)
                    {
                        baseSymbol = symbols[i].GetId();
                        if (startIndex == -1)
                        {
                            startIndex = i;
                        }
                    }
                    comboLength++;
                }
                else
                {
                    if (comboLength >= 3)
                    {
                        endIndex = i - 1;
                        totalPoints = symbols.Skip(startIndex).Take(endIndex - startIndex + 1).Sum(s => s.GetPoints());
                        return true;
                    }

                    comboLength = 0;
                    baseSymbol = null;
                    startIndex = -1;
                    endIndex = -1;

                    if (!symbols[i].IsWild())
                    {
                        baseSymbol = symbols[i].GetId();
                        startIndex = i;
                        comboLength = 1;
                    }
                }
                
                if (i == symbols.Length - 1 && comboLength >= 3)
                {
                    endIndex = i;
                    totalPoints = symbols.Skip(startIndex).Take(endIndex - startIndex + 1).Sum(s => s.GetPoints());
                    return true;
                }
            }

            return false; 
        }

        private IEnumerator MoveTrail(GameObject trail, ISymbol[] symbols)
        {
            trail.SetActive(false);
            
            for (int i = 0; i < symbols.Length; i++)
            {
                symbols[i].GetSpriteBeground().gameObject.SetActive(true);
            }

            Vector3 startPosition = symbols[0].GetTransform().position;
            Vector3 endPosition = symbols[symbols.Length - 1].GetTransform().position;

            float elapsedTime = 0f;
            float journeyLength = Vector3.Distance(startPosition, endPosition);

            trail.transform.position = startPosition;
            trail.SetActive(true);

            while (elapsedTime < journeyLength / trailSpeed)
            {
                trail.transform.position = Vector3.Lerp(startPosition, endPosition, (elapsedTime * trailSpeed) / journeyLength);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            trail.transform.position = endPosition;
        }

        private void ResetTrails()
        {
            foreach (var trail in trails)
            {
                trail.SetActive(false);
                TrailRenderer trailRenderer = trail.GetComponent<TrailRenderer>();
                if (trailRenderer != null)
                {
                    trailRenderer.Clear();
                }
            }
        }

        private void UpdateScore(int additionalPoints)
        {
            int currentScore = int.Parse(score.text);
            currentScore += additionalPoints;
            score.text = currentScore.ToString();
        }

        private IEnumerator StartReelWithDelay(Reel reel, float spinDuration, float delay)
        {
            yield return new WaitForSeconds(delay); 
            reel.Spin(spinDuration, spinSpeed);  
        }

        private void UnSubscribeEvents()
        {
            foreach (var t in reels)
            {
                t.ReelsStopedEvent -= ReelStopped;
            }
        }

        private void SubscribeEvents()
        {
            foreach (var t in reels)
            {
                t.ReelsStopedEvent += ReelStopped;
            }
        }
    }
}
