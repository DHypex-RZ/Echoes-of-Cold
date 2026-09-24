using UnityEngine;
using UnityEngine.Events;

namespace HUD
{
    public class PauseController : MonoBehaviour
    {
        public static PauseController Instance { get; private set; }
        
        public UnityEvent show;
        public UnityEvent hide;

        bool _inPause;

        void Start() {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false; 
            if (Instance == null) Instance = this; }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _inPause = !_inPause;
                
                if (_inPause) {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    Time.timeScale = 0f;
                    show.Invoke();
                    //Time.timeScale = 0;
                }
                else {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    Time.timeScale = 1f;
                    hide.Invoke();
                    //Time.timeScale = 1;
                }
            }
        }
    }
}