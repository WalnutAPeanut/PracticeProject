using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KMProject
{
    public class CreateSlots : MonoBehaviour
    {
        public static readonly int Horizon = 7;
        public static readonly int Vertical = 8;
        public static Slot[][] slots;

        public Transform BaseParent;

        public Slot SlotPrefab;
        public GameObject iconPrefab;

        public Sprite[] iconImages;

        public static Slot[] createSlot;
        void Start()
        {
            slots = new Slot[Vertical][];
           
            for (int i = 0; i < Vertical; ++i)
            {
                int y = i * -50 + 175;
                slots[i] = new Slot[Horizon];
                for (int j = 0; j < Horizon; ++j)
                {
                    int x = j * 48 - 144;
                    Slot slot = Instantiate(SlotPrefab);
                    slot.transform.parent = transform;
                    slot.transform.localPosition = new Vector3Int(x, y, 0);
                    slot.createSlotIndex = j;
                    slots[i][j] = slot;
                }
            } 
            for (int i = 0; i < Vertical; ++i)
            {
                for (int j = 0; j < Horizon; ++j)
                {
                    slots[i][j].SetNeighborhood(this, j, i);
                }
            }
            createSlot = new Slot[Horizon];
            for (int i = 0; i < Horizon; ++i)
            {
                createSlot[i] = Instantiate(SlotPrefab, transform);
                createSlot[i].currentType = Slot.ESlottype.Respone;
                createSlot[i].transform.localPosition = new Vector3Int(i * 48 - 144, 225, 0);
                createSlot[i].SetNeighborhood(Slot.EDirection.D, slots[0][i]);
                createSlot[i].createIconCount = Vertical;
                createSlot[i].BaseParent = BaseParent;
            }
        }
        public Slot GetNeighborhood(int y, int x)
        {
            if (y < 0 || x < 0) return null;
            if (x >= Horizon || y >= Vertical) return null;
            return slots[y][x];
        }
    }
}
