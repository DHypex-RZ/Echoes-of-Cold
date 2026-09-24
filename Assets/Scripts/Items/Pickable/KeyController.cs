using Player;
using UnityEngine;
using UnityEngine.Events;
using inputs = Inputs.InputController;

namespace Items.Pickable
{
   public class KeyController : ItemController
   {
      [SerializeField] UnityEvent actions;

      void Update()
      {

         if (!inputs.KeyE || !playerIsPresent) return;

         actions.Invoke();
         enabled = false;
      }

      public void ActiveKey()
      {
         actions.Invoke();
         
        }
   }
}