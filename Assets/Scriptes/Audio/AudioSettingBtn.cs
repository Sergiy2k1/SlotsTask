using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scriptes.Audio
{
    public class AudioSettingBtn : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentState;
        
        private Button _button;

        private void Awake()
        {
            Initial();
        }

        private void Start()
        {
            if (_button != null)
            {
                _button.onClick.AddListener(ToggleAudio);
            }

            UpdateAudioStateText();
        }

        private void OnDisable()
        {
            if (_button != null)
            {
                _button.onClick.RemoveAllListeners();
            }
        }

        private void Initial() 
        {
            _button = GetComponent<Button>();
            
            if (_button == null)
            {
                Debug.LogError("Button component is missing on the GameObject.");
            }
        }

        private void UpdateAudioStateText() => 
            currentState.text = AudioManager.Instance.IsAudioMuted ? "Off" : "On";

        private void ToggleAudio()
        {
            AudioManager.Instance.ToggleAudio();
            UpdateAudioStateText();
        }
    }
}