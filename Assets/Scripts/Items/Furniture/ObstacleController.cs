using System;
using Items.Pickable;
using UnityEngine;
using UnityEngine.Events;
using inputs = Inputs.InputController;

namespace Items.Furniture
{
    public class ObstacleController : ItemController
    {
        [SerializeField] PickableController itemRequired;
        [SerializeField] UnityEvent actionsWithInteraction;
        [SerializeField] UnityEvent actionsWithoutItem;

        void Update()
        {
            if (itemRequired)
            {
                if (itemRequired.InHand && inputs.KeyE && playerIsPresent) actionsWithInteraction.Invoke();
                else if (playerIsPresent && !itemRequired.InHand) actionsWithoutItem.Invoke();
            }
            else
            {
                if (playerIsPresent && inputs.KeyE) actionsWithInteraction.Invoke();
            }
        }

        void OnDisable() { actionsWithoutPlayer.Invoke(); }
    }
}
