using Player;
using UnityEngine;
using inputs = Inputs.InputController;

namespace Items.Pickable
{
    public class PickableController: ItemController
    {
        static PickUpController _Player;

        [SerializeField] Vector3 rotationAtPicking;

        public bool InHand { get; private set; }
        
        void Start(){ if (_Player == null) _Player = GameObject.Find("Player").GetComponent<PickUpController>(); }
        
        void Update()
        {
            if (InHand) return; 
            if (!inputs.KeyE || !playerIsPresent) return;
            
            _Player.PickUp(gameObject, rotationAtPicking);
            actionsWithoutPlayer.Invoke();
            InHand = true;
        }
    }
}