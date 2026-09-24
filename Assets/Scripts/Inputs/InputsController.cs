using UnityEngine;
using static UnityEngine.Input;
using static UnityEngine.KeyCode;

namespace Inputs
{
    public class InputController : MonoBehaviour
    {
        public static float MouseX { get; private set; }
        public static float MouseY { get; private set; }
        public static Vector3 Direction { get; private set; }
        public static bool Shift {get; private set;}
        public static bool KeyE { get; private set; }
        public static bool KeyEsc {get; private set;}
        public static float MouseWheel { get; private set; }

        void Update()
        {
            MouseX += GetAxis("Mouse X") * Time.deltaTime;
            MouseY -= GetAxis("Mouse Y") * Time.deltaTime;
            MouseY = Mathf.Clamp(MouseY, -1, 1);

            Direction = new Vector3(GetAxis("Horizontal"), 0, GetAxis("Vertical")).normalized;
            Shift = GetButton("Run");
            KeyE = GetButtonDown("Interaction");
            KeyEsc = GetButtonDown("Cancel");
            MouseWheel = GetAxis("Mouse ScrollWheel");
        }
    }
}