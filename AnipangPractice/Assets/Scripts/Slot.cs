using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KMProject
{
    public class Slot : MonoBehaviour
    {
        ///슬롯 기본정보들
        private int index;
        public int Index { set { index = value; } get { return index; } }
        public Rect rect { get { return rcTransform.rect; } }

        public Sprite slotImage;
        private Icon currentIcon;
        public Icon CurrentIcon;
        private RectTransform rcTransform;

        private void Awake()
        {
            rcTransform = transform as RectTransform;
        }


        public void ChangeImage()
        {

        }
    }
}