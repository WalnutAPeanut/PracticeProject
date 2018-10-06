﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KMProject
{
    public class InputManager : MonoBehaviour
    {
        private Slot selectSlot;
        //        private Vector3 offset = new Vector3(-176f, 313f, 0f);
        private Vector3 offset = new Vector3(-160f, 293f, 0f);
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                selectSlot = PtInRect();
            }
            else if (Input.GetMouseButton(0))
            {
                if(selectSlot != null)
                {
                    Slot result = PtInRect();
                    if (result != selectSlot)
                    {
                        /// 대각선이동도 가능함. 막는방법을 생각해보자.
                        selectSlot.ChangeIcon(result);
                        selectSlot = null;
                    }
                }
            }
            if (Input.GetMouseButton(1))
            {
                if (selectSlot != null)
                {
                    selectSlot.ChangeIconImage(1);
                }
            }
        }
        private Slot PtInRect()
        {
            Vector3 mousePos = Input.mousePosition;
          //  Camera.main.point
            for (int i = 0; i < CreateSlots.Vertical; ++i)
            {
                for (int j = 0; j < CreateSlots.Horizon; ++j)
                {
                    if (CreateSlots.slots[i][j].isInBoxPoint(mousePos, offset))
                    {
                        CreateSlots.slots[i][j].ChangeIconImage(2);
                        return CreateSlots.slots[i][j];
                    }
                }
            }
            return null;
        }
    }
}

