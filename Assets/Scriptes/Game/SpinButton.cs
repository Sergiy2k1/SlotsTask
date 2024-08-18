using Scriptes.Audio;
using Scriptes.Const;
using UnityEngine;
using UnityEngine.UI;

namespace Scriptes.Game
{
    public class SpinButton : MonoBehaviour
    {
        [SerializeField] private Button spinButton; 
        

        private void Start() => 
            spinButton.onClick.AddListener(OnSpinButtonClicked);

        private void OnSpinButtonClicked() => 
            AudioManager.Instance.PlaySFX(AudioConst.ButtonClick);
    }
}