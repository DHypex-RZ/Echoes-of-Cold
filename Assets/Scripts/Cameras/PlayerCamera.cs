using UnityEngine;
using inputs = Inputs.InputController;

namespace Cameras
{
    public class PlayerCamera : MonoBehaviour
    {
        public static PlayerCamera Instance;
        
        [Tooltip("Eje de rotación")]
        [SerializeField] Transform pivot;
        
        [Header("Configuración de la cámara")]
        [SerializeField] float rotationXSpeed;
        [SerializeField] float rotationYSpeed;
        [SerializeField] float rangeYRotation;

        [Header("Configuración del Head Bobbing")]
        [SerializeField] bool activeBobbing;
        [SerializeField] Transform bobPivot;
        [SerializeField] float frequency;
        [SerializeField] float amplitude;
        [SerializeField] float smoothBobbing;

        [Header("Configuración del Sway")] 
        [SerializeField] bool activeSway;
        [SerializeField] Transform swayPivot;
        [SerializeField] float step;
        [SerializeField] float maxStepDistance;
        [SerializeField] float rotationStep;
        [SerializeField] float maxRotationStep;
        [SerializeField] float smoothSway;
        
        
        
        [Header("Configuración de la interacción")]
        [SerializeField] float interactionDistance;
        [SerializeField] LayerMask interactionLayers;

        float _timer;
        Vector3 _pivotPosition;
        
        public Camera Camera { get; private set; }

        void Awake() { if (Instance == null) Instance = this; }

        void Start()
        {
            Camera = Camera.main;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _pivotPosition = pivot.localPosition;
        }

        void Update()
        {
            float mouseX = inputs.MouseX * rotationXSpeed;
            float mouseY = inputs.MouseY * rotationYSpeed;

            mouseY = Mathf.Clamp(mouseY, -rangeYRotation, rangeYRotation);

            transform.localRotation = Quaternion.Euler(transform.rotation.x, mouseX, transform.rotation.z);
            pivot.localRotation = Quaternion.Euler(mouseY, pivot.rotation.y, pivot.rotation.z);
            
            if (activeBobbing) HandleHeadBobbing();
            if (activeSway) HandleSway();
        }
        
        void HandleHeadBobbing()
        {
            if (inputs.Direction.magnitude > 0.1f)
            {
                _timer += Time.deltaTime * frequency;

                float bobY = Mathf.Sin(_timer) * amplitude;
                float bobX = Mathf.Cos(_timer * 0.5f) * (amplitude * 0.5f);

                Vector3 targetPosition = _pivotPosition + new Vector3(bobX, bobY, _pivotPosition.z);
                bobPivot.localPosition = Vector3.Lerp(bobPivot.localPosition, targetPosition, Time.deltaTime * smoothBobbing);
            }
            else
            {
                _timer = 0;
                pivot.localPosition = Vector3.Lerp(pivot.localPosition,_pivotPosition, Time.deltaTime * smoothBobbing);
            }
        }

        void HandleSway()
        {
            Vector3 look = new(inputs.MouseX * step, inputs.MouseY * step);
            
            look.x = Mathf.Clamp(look.x, -maxStepDistance, maxStepDistance);
            look.y = Mathf.Clamp(look.y, -maxStepDistance, maxStepDistance);
            
            Vector3 targetPosition = new(look.y, look.x, look.x);
            
            swayPivot.localPosition = Vector3.Lerp(swayPivot.localPosition, targetPosition, Time.deltaTime * smoothSway);
            
            look = new(inputs.MouseX * rotationStep, inputs.MouseY * rotationStep);

            look.x = Mathf.Clamp(look.x, -maxRotationStep, maxRotationStep);
            look.y = Mathf.Clamp(look.y, -maxRotationStep, maxRotationStep);

            Quaternion targetRotation = Quaternion.Euler(look.y, look.x, look.x);
            
            swayPivot.localRotation = Quaternion.Lerp(swayPivot.localRotation, targetRotation, Time.deltaTime * smoothSway);
        }
        
        

        public bool HandleInteraction(out RaycastHit hit)
        {
            Ray ray = new(Camera.transform.position, Camera.transform.forward);
            
            Debug.DrawRay(Camera.transform.position, Camera.transform.forward * interactionDistance, Color.green, 0.25f);

            return Physics.Raycast(ray, out hit, interactionDistance, interactionLayers);
        }
    }
}