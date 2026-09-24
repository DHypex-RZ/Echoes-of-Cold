using Cameras;
using UnityEngine;
using UnityEngine.Events;

namespace Items
{
    public abstract class ItemController: MonoBehaviour
    {
        protected PlayerCamera player;
        
        [Header("Jugador entra al Trigger")]
        [SerializeField] protected UnityEvent actionsWithPlayer;
        
        [Header("Jugador deja el Trigger")]
        [SerializeField] protected UnityEvent actionsWithoutPlayer;
        
        private protected bool playerIsPresent;
        
        void Start(){ player = PlayerCamera.Instance; }
        
        protected virtual void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            playerIsPresent = true;
            actionsWithPlayer.Invoke();
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            playerIsPresent = false;
            actionsWithoutPlayer.Invoke();
        }
    }
}