﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KMProject
{
    public class Slot : MonoBehaviour
    {
        public Icon IconPrefab;
        public enum ESlottype { Respone, Basic }
        public enum EDirection { L, LU, U, RU, R, RD, D, LD }
        public Slot[] neighborhood = new Slot[8];

        public Icon currentIcon;
        public int createSlotIndex;

        public ESlottype currentType = ESlottype.Basic;
        public int createIconCount;

        public Transform BaseParent;
        private RectTransform rcTransform;
        private void OnEnable()
        {
            rcTransform = transform as RectTransform;
            StartCoroutine(CreateIcon());
            if (currentType == ESlottype.Respone)
                createIconCount = 1;
        }
        public void SetNeighborhood(CreateSlots cslot, int x, int y)
        {
            neighborhood[(int)EDirection.L] = cslot.GetNeighborhood( y, x - 1);
            neighborhood[(int)EDirection.LU] = cslot.GetNeighborhood(y - 1, x - 1);
            neighborhood[(int)EDirection.U] = cslot.GetNeighborhood(y - 1, x);
            neighborhood[(int)EDirection.RU] = cslot.GetNeighborhood(y - 1, x + 1);
            neighborhood[(int)EDirection.R] = cslot.GetNeighborhood(y, x + 1);
            neighborhood[(int)EDirection.RD] = cslot.GetNeighborhood(y + 1, x + 1);
            neighborhood[(int)EDirection.D]= cslot.GetNeighborhood(y + 1, x);
            neighborhood[(int)EDirection.LD] = cslot.GetNeighborhood(y + 1 , x - 1);
        }
        public void SetNeighborhood(EDirection eDirection, Slot slot)
        {
            neighborhood[(int)eDirection] = slot;
        }
        public Slot FindTargetSlot()
        {
            Slot down = neighborhood[(int)EDirection.D];
            if (down == null)
            {
                if (currentIcon == null)
                    return this;
            }
            else if(down.currentIcon)
            {
                if(down.currentIcon.StartFindTargetSlot())
                {
                    down.currentIcon = null;
                    return down;
                }
                return this;
            }
            else
            {
                return down.FindTargetSlot();
            }
            return null;
        }
        public void RequireIcon()
        {
            Invoke("DelayStart", 0.16f);
        }

        void DelayStart()
        {
            CreateSlots.createSlot[createSlotIndex].createIconCount++;
        }
        private Slot FindDeepSlot()
        {
            if (neighborhood[(int)EDirection.D] == null)
                return this;

            return null;
        }
        private IEnumerator CreateIcon()
        {
            while(true)
            {
                if(createIconCount > 0)
                {
                    createIconCount--;
                    Icon icon = Instantiate(IconPrefab, transform.position, transform.rotation, BaseParent);
                    icon.targetSlot = this;
                    icon.StartFindTargetSlot();
                }
                yield return null;
            }
        }
        public void CheckIcon()
        {
            currentIcon.LineCheck();
        }

        public bool isInBoxPoint(Vector3 position, Vector3 offset)
        {
            Rect rc = rcTransform.rect;
            Vector3 size = new Vector3(rc.width * 0.5f, rc.height * 0.5f, 0f);
            Vector3 pos = rcTransform.localPosition;
            pos.x = pos.x - offset.x;
            pos.y = pos.y + offset.y;

            Vector3 max = pos + size;
            Vector3 min = pos - size;
            
            if(position.x > max.x || position.x < min.x || position.y > max.y || position.y < min.y)
                return false;
            return true;
        }

        public void ChangeIcon(Slot target)
        {
            //가끔 Null 뜸 원인을 찾아보자
            target.currentIcon.targetSlot = this;
            currentIcon.targetSlot = target;
            Icon temp = target.currentIcon;
            target.currentIcon = currentIcon;
            currentIcon = temp;


            ///움직인 아이콘을 pivot으로 잡고 두곳다 lineCheck가 끝날경구 그때 아이콘을 다같이 없애준다.
            ///아이콘의 변경 방향 체크 X가 바뀌었는가? Y가 바뀌었는가? 4개일떄 변형을 위하여 체크함.
         //   currentIcon.LineCheck();
            target.currentIcon.LineCheck();
            ///움직인 아이콘이 라인체크한후 없어질 객체가 없다면 다시 원상태로 돌아가야한다.
        }

        public void ChangeIconImage(int index)
        {
            currentIcon.ChagneImage(index);
        }
    }
}