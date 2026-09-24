using UnityEngine;
using UnityEngine.Events;
using inputs = Inputs.InputController;

namespace Items.Furniture
{
    public class FurnitureController: ItemController
    {
        static FurnitureController _Current;
        
        [Header("Objeto")]
        [SerializeField] GameObject furniture;
        
        [Header("Interacción del jugador")]
        [SerializeField] UnityEvent actionWhenIsClose;
        [SerializeField] UnityEvent actionWhenIsOpen;
        
        bool _isClose = true;

        void Awake() { if (_Current == null) _Current = this; }

        protected virtual void Update()
        {
            if (!playerIsPresent) return;

            if (!inputs.KeyE || _Current != this) return;

            if (_isClose) actionWhenIsClose.Invoke();
            else actionWhenIsOpen.Invoke();

            _isClose = !_isClose;
        }

        void OnTriggerStay(Collider other)
        {
            if (!playerIsPresent) return;
            
            if (!PlayerIsSeeing())
            {
                if (_Current == this) _Current = null;
                
                return;
            }
            
            _Current = this;
            actionsWithPlayer.Invoke();
        }

        bool PlayerIsSeeing()
        {
            if (player.HandleInteraction(out RaycastHit hit))
                return hit.collider.gameObject == furniture;

            return false;
        }
    }
}