using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KMProject
{
    /// 아이콘은 오로지 보여주기 위한 객체로로 만든다?
    /// 움직임은 슬롯에서 관리
    /// 자신 삭제도 슬롯에서 관리
    public class Icon : MonoBehaviour
    {
        public static Transform BaseParent;
        public Sprite[] images;
        public Image image;
        public Slot targetSlot;
        public int inconIndex;

        private void OnEnable()
        {
            inconIndex = Random.Range(0, 3);
            ///아이콘이 생성되어짐.
            ///목표 Slot이 정해짐.
        }
        private void OnDisable()
        {
            targetSlot.RequireIcon();
        }
        private void Update()
        {
            if (targetSlot)
                transform.localPosition = Vector3.Lerp(transform.localPosition, targetSlot.transform.localPosition, Time.deltaTime * 3f);
        }

        public bool StartFindTargetSlot()
        {
            Slot s = targetSlot.FindTargetSlot();
            if (s == targetSlot) return false;
            if (s == null) return false;
            targetSlot = s;
            targetSlot.currentIcon = this;
            transform.localScale = Vector3.one;

            return true;
        }
        public bool IconEqual(int value)
        {
            return inconIndex == value;
        }
        public void LineCheck()
        {
            ///아이콘 한개 체크. 기본적을 2번 체크해야함.
            IconManager.Instance.pivot = this;
            /// ㅡ 방향
            CheckArr(Slot.EDirection.R);
            CheckArr(Slot.EDirection.L);
            IconManager.Instance.Check();
            //3개이상이다 Icon clear 작업.
            ///// ㅣ 방향
            CheckArr(Slot.EDirection.U);
            CheckArr(Slot.EDirection.D);
            IconManager.Instance.Check();
            ////3개이상이다 Icon clear 작업.
            ///// \ 방향
            //CheckArr(Slot.EDirection.LU);
            //CheckArr(Slot.EDirection.RD);
            ////3개이상이다 Icon clear 작업.
            ///// / 방향
            //CheckArr(Slot.EDirection.RU);
            //CheckArr(Slot.EDirection.LD);
            ////3개이상이다 Icon clear 작업.
            
            ///모든 검사가 끝났으므로 아이콘을 지울것인지 바꿀것인지 체크한다.
            IconManager.Instance.EndCheck();
        }
        private void CheckArr(Slot.EDirection direction)
        {
            if (targetSlot == null) return;
            Slot slot = targetSlot.neighborhood[(int)direction];
            if (slot == null) return; //현재방향의 끝부분
            if (slot.currentIcon.IconEqual(inconIndex))
            {
                //같은 방향으로 같은것이 있다. 카운팅 하고 다음검사.
                IconManager.Instance.AddIcon(slot. currentIcon);
                slot.currentIcon.CheckArr(direction);
            }
        }

        public void ChagneImage(int _index)
        {
            image.sprite = images[_index];
        }
        public void Remove()
        {
            Destroy(gameObject);
        }
    }
}
