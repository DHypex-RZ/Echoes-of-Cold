using System;
using HUD;
using UnityEngine;
using inputs = Inputs.InputController;

namespace Items.Pickable
{
    public class HintController : ItemController
    {

        [SerializeField] protected Sprite hintImage;
        [SerializeField] protected string hintText;
        [SerializeField] protected string hintDescription;
        [SerializeField] protected float hintTimeScale;

        protected InteractionController canvas;
        protected ViewerController viewer;

        protected virtual void Start()
        {
            canvas = InteractionController.Instance;
            viewer = ViewerController.Instance;
            hintTimeScale = 1f;
        }

        async void Update()
        {
            if (!inputs.KeyE || !playerIsPresent) return;

            canvas.ShowHint(hintText, hintDescription, hintImage);
            viewer.ShowInCanvas(hintImage);
            
            hintTimeScale = Time.timeScale;
            actionsWithoutPlayer.Invoke();
            
            if (TryGetComponent(out KeyController key))
            {
                key.ActiveKey();
                
            }
            
            Destroy(gameObject);
        }

        /*async void OnDestroy()
        {
            enabled = false;
            GetComponent<Renderer>().enabled = false;
            await Awaitable.WaitForSecondsAsync(1);
        }*/
    }
}
