using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KMProject
{
    public class ResponeSlot : Slot
    {
        public override ESlottype GetSlotType { get { return ESlottype.Respone; } }
        public int createIconCount;
        public Transform BaseParent;

        private void OnEnable()
        {
            rcTransform = transform as RectTransform;
            StartCoroutine(CreateIcon());
        }

        private IEnumerator CreateIcon()
        {
            while (true)
            {
                if (createIconCount > 0)
                {
                    createIconCount--;
                    Icon icon = Instantiate(IconPrefab, transform.position, transform.rotation, BaseParent);
                    icon.targetSlot = this;
                    icon.StartFindTargetSlot();
                }
                yield return null;
            }
        }

       
    }
}
