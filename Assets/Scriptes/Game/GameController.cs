using UnityEngine;
using UnityEngine.UI;

namespace Scriptes.Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameBoard gameBoard; 
        [SerializeField] private Button spinButton;  

        private void Start()
        {
            spinButton.onClick.AddListener(OnSpinButtonPressed);
        }

        public void OnSpinButtonPressed()
        {
            gameBoard.SpinReels();
            spinButton.interactable = false;
        }
    }
}