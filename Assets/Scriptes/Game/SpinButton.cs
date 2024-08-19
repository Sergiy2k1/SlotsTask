using Scriptes.Audio;
using Scriptes.Const;
using UnityEngine;
using UnityEngine.UI;

namespace Scriptes.Game
{
    public class SpinButton : MonoBehaviour
    {
        [SerializeField] private GameBoard gameBoard; 
        [SerializeField] private Button spinButton;  

        private void Start()
        {
            
            spinButton.onClick.AddListener(OnSpinButtonPressed);
            gameBoard.StartSpinEvent += DisableSpinButton;
            gameBoard.StopSpinEvent += EnableSpinButton;
        }

        private void OnDisable()
        {
            gameBoard.StartSpinEvent -= DisableSpinButton;
            gameBoard.StopSpinEvent -= EnableSpinButton;
        }

        private void EnableSpinButton() =>
            spinButton.interactable = true;

        private void DisableSpinButton() => 
            spinButton.interactable = false;

        private void OnSpinButtonPressed()
        {
            AudioManager.Instance.PlaySFX(AudioConst.ButtonClick);
            gameBoard.SpinReels();
        }
        
    }
}