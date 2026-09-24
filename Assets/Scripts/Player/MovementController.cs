using UnityEngine;
using inputs = Inputs.InputController;

namespace Player
{
    public class MovementController: MonoBehaviour
    {
        [Header("Velocidad de movimiento")]
        [SerializeField] float walkSpeed;
        [SerializeField] float runSpeed;
        
        [Header("Energía del jugador")]
        [SerializeField] float stamina;
        [SerializeField] float drainPerSecond;
        [SerializeField] float regenerationPerSecond;
        [SerializeField] float delayToRegenerate;
        [SerializeField] float exhaustionThreshold;
        
        Rigidbody _rb;
        float _currentStamina;
        float _timer;
        bool _isExhausted;
        
        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _currentStamina = stamina;
        }
        
        void Update() { HandleStamina(); }

        void FixedUpdate() { Movement(); }
      
        void Movement()
        {
            bool isRunning = inputs.Shift && inputs.Direction != Vector3.zero;
            bool canRun = _currentStamina > 0 && !_isExhausted;
            float speed = isRunning && canRun ? runSpeed : walkSpeed;
         
            Vector3 dir = transform.TransformDirection(inputs.Direction);
            Vector3 velocity = new(dir.x * speed, _rb.linearVelocity.y, dir.z * speed);
         
            _rb.linearVelocity = velocity;
            _rb.MoveRotation(Quaternion.Euler(_rb.rotation.x, inputs.MouseX, _rb.rotation.z));
        }

        void HandleStamina()
        {
            bool isRunning = inputs.Shift && inputs.Direction != Vector3.zero && _currentStamina > 0f;

            if (isRunning && !_isExhausted)
            {
                _currentStamina -= drainPerSecond * Time.deltaTime;
                _timer = 0f;

                if (!(_currentStamina <= 0f)) return;
            
                _currentStamina = 0f;
                _isExhausted = true;
            }
            else
            {
                if (_currentStamina >= stamina) _currentStamina = stamina;

                _timer += Time.deltaTime;

                if (_timer < delayToRegenerate) return;
            
                _currentStamina = Mathf.MoveTowards(_currentStamina, stamina, regenerationPerSecond * Time.deltaTime);

                if (_isExhausted && _currentStamina >= exhaustionThreshold) _isExhausted = false;
            }
        }

        internal float StaminaPercentage() { return _currentStamina / stamina; }
    }
}