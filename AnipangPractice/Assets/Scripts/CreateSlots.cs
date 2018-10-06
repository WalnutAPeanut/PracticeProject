using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KMProject
{
    public class CreateSlots : MonoBehaviour
    {
        public Slot slotPrefab;
        public Vector2Int offset;
        public Vector2Int size;
        public Vector2Int count;
        private Slot[] slots;
        private void Start()
        {
            slots = new Slot[count.x * count.y];

            for (int i = 0; i < count.x; ++i)
            {
                int y = i * size.y + offset.y;
                for(int j = 0; j < count.y; ++j)
                {
                    int x = j * size.x + offset.x;
                    Slot slot = Instantiate<Slot>(slotPrefab, transform);
                    slot.transform.localPosition = new Vector3(x, y, 0f);
                    slot.Index = i * count.x + j;
                    slots[slot.Index] = slot;
                }
            }
        }

        private Slot selectSlot;
        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(selectSlot)
                    selectSlot.ChangeImage();
                selectSlot = FindSlot(slots, Input.mousePosition);
                selectSlot.ChangeImage();
            }
            else if (Input.GetMouseButton(0))
            {

            }
        }
        public bool PtInRect(Vector3 point, Rect rect, Vector3 size)
        {
            Vector2 halfSize = size * 0.5f;
            Vector2 min = rect.position - halfSize;
            Vector2 max = rect.position + halfSize;

            if (point.x > max.x || point.x < min.x || point.y > max.y || point.y < min.y)
                return false;
            return true;
        }
        public Slot FindSlot(Slot[] _slots, Vector3 mousePos)
        {
            int l = _slots.Length;
            for(int i = 0; i < l; ++i)
            {
                if(PtInRect(mousePos, _slots[i].rect, _slots[i].CurrentIcon.ImageSize))
                    return _slots[i];
            }
            return null;
        }
    }
}
