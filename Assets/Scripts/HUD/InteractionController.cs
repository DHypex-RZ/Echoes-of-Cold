using TMPro;
using UnityEngine;
using UnityEngine.UI;
using inputs = Inputs.InputController;

namespace HUD
{
    public class InteractionController : MonoBehaviour
    {
        public static InteractionController Instance { get; private set; }
        [SerializeField] GameObject interactionText;
        [SerializeField] GameObject canvas;
        [SerializeField] TMP_Text hintName;
        [SerializeField] TMP_Text hintDescription;
        [SerializeField] Image hintImage;
        [SerializeField] GameObject blockedText;
        [SerializeField] GameObject musicText;
        bool _showingCanvas;

        void Awake() {if (Instance == null) Instance = this;}
        
        void Start() { canvas.SetActive(false); }

        void Update()
        {
            if (_showingCanvas && inputs.KeyEsc)
            {
                Time.timeScale = 1f;
                canvas.SetActive(false);
                PauseController.Instance.enabled = true;
            }
        }

        public void ActiveText(bool active) { interactionText.SetActive(active); }
        public void ActiveBlockedText(bool active) { blockedText.SetActive(active); }

        public void ShowHint(string name, string description, Sprite image)
        {
            hintName.text = name;
            hintDescription.text = description;
            hintImage.sprite = image;
            _showingCanvas = true;
            Time.timeScale = 0f;
            
            PauseController.Instance.enabled = false;
            canvas.SetActive(true);
        }

        public async void ShowHintToMusicBox()
        {
            musicText.SetActive(true);
            await Awaitable.WaitForSecondsAsync(2);
            musicText.SetActive(false);
        }
    }
}
