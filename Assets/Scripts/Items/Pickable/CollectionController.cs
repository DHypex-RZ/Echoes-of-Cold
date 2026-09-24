using UnityEngine;
using UnityEngine.Events;
using inputs = Inputs.InputController;

namespace Items.Pickable
{
    public class CollectionController : HintController
    {
        public static bool AllIsCollected { get; private set; }
        [SerializeField] CollectionController[] items;
        [SerializeField] UnityEvent actions;
        [SerializeField] UnityEvent actionsAtPick;
        bool _isCollected;

        MeshRenderer _renderer;
        BoxCollider _collider;


        protected override void Start()
        {
            _renderer = GetComponent<MeshRenderer>();
            _collider = GetComponent<BoxCollider>();

            base.Start();
        }

        void Update()
        {
            bool allItemsCollected = true;

            if (!inputs.KeyE || !playerIsPresent) return;

            _isCollected = true;
            actionsAtPick.Invoke();

            foreach (var item in items)
            {
                allItemsCollected &= item._isCollected;
            }

            if (allItemsCollected)
            {
                playerIsPresent = false;
                canvas.ShowHint(hintText, hintDescription, hintImage);
                viewer.ShowInCanvas(hintImage);
                actionsWithoutPlayer.Invoke();
                AllIsCollected = true;
                actions.Invoke();
            }

            enabled = false;
            _renderer.enabled = false;
            _collider.enabled = false;
        }
    }
}