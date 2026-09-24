using Items.Pickable;
using UnityEngine;
using UnityEngine.Events;
using inputs = Inputs.InputController;

namespace Items.Furniture
{
   public class BlockedController : FurnitureController
   {
      bool _blocked = true;

      [SerializeField] UnityEvent actionsWithPlayerAndBlocked;
      [SerializeField] UnityEvent actionsWithoutPlayerAndBlocked;

      public void Unlock(bool unlock)
      {
         _blocked = !unlock;
      }


      protected override void Update()
      {
         if (_blocked)
         {
            return;
         }

         base.Update();
      }

      protected override void OnTriggerEnter(Collider other)
      {
         if (!other.CompareTag("Player")) return;

         playerIsPresent = true;

         if (_blocked)
         {
            actionsWithPlayerAndBlocked.Invoke();
         }
         else
         {
            actionsWithPlayer.Invoke();
         }


      }

      protected override void OnTriggerExit(Collider other)
      {
         if (!other.CompareTag("Player")) return;

         playerIsPresent = false;

         if (_blocked)
         {
            actionsWithoutPlayerAndBlocked.Invoke();
         }
         else
         {
            actionsWithoutPlayer.Invoke();
         }

      }
   }
}