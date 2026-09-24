using System;
using System.Collections.Generic;
using UnityEngine;
using inputs = Inputs.InputController;

namespace Player
{
    public class PickUpController: MonoBehaviour
    {
        [SerializeField] Transform pivot;
        [SerializeField] List<GameObject> items;

        GameObject _itemSelected;
        int _index = -1;

        void Update()
        {
            if (items.Count == 0) return;
            
            if (inputs.MouseWheel > 0.01f) ChangeItem(1);
            if (inputs.MouseWheel < -0.01f) ChangeItem(-1);
        }

        public void PickUp(GameObject item, Vector3 rotation)
        {
            items.Add(item);

            item.transform.SetParent(pivot);
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.Euler(rotation);
            _index = items.Count - 1;
            
            SelectItem();
        }

        void ChangeItem(int direction)
        {
            _index += direction;
            
            if (_index >= items.Count) _index = 0;
            if (_index < 0) _index = items.Count - 1;

            SelectItem();
        }

        void SelectItem() { for (int i = 0; i < items.Count; i++) items[i].SetActive(i == _index); }
    }
}