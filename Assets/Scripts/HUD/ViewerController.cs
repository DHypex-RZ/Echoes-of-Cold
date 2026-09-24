using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HUD
{
    public class ViewerController: MonoBehaviour
    {
        public static ViewerController Instance { get; private set; }
        
        [SerializeField] RectTransform origin;
        [SerializeField] Vector2 offset;
        [SerializeField] Image referenceToFrame;
        [SerializeField] Image referenceToImage;
        [SerializeField] TMP_Text text;
        [SerializeField] TMP_Text text2;
        int _count;
        int _count2;
        Vector2 _location;

        void Awake() { if (Instance == null) Instance = this; }

        public void ShowInCanvas(Sprite image)
        {
           /*if (_count != 0) _location += offset;

            Image aux = Instantiate(referenceToFrame, origin);
            Image img = Instantiate(referenceToImage, origin);
            img.sprite = image;
            img.rectTransform.anchoredPosition = _location;
            aux.rectTransform.anchoredPosition = _location;

            aux.gameObject.SetActive(true);
            img.gameObject.SetActive(true);
            
            _count++;*/
           _count++;
           text.text = _count.ToString();
        }

        public void AddCount()
        {
            _count2++;
            text2.text = _count2.ToString();
        }
    }
}