using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KMProject
{
    public class IconManager : MonoBehaviour
    {
        private static IconManager instance;
        public static IconManager Instance
        {
            get
            {
                return instance;
            }
        }

        public Icon pivot;
        public List<Icon> listCheckIcon = new List<Icon>();
        public List<Icon> listResertIcon = new List<Icon>();

        /// 3개 이상이면 아이콘을 지워야 하므로 그때 등록한다.
        public List<Icon> listEraserIcon = new List<Icon>();
        private bool pivotDelete;
        // Use this for initialization
        void Awake()
        {
            instance = this;
        }
        private void OnDestroy()
        {
            instance = null;
        }
        public void AddIcon(Icon icon)
        {
            listCheckIcon.Add(icon);
        }

        public void Check()
        {
            ///한 라인의 지울 개체수를 체크한다.
            ///지울 아이콘이 2개 이상이라면
            if (listCheckIcon.Count > 1) ///pivot은 기본으로 지운다고 생각한다.
            {
                ///마지막에 지울 리스트에 등록한다.
                for (int i = 0; i < listCheckIcon.Count; ++i)
                {
                    listEraserIcon.Add(listCheckIcon[i]);
                }
            }
            listCheckIcon.Clear();
        }
        public void Result()
        {
            for(int i = 0; i < listResertIcon.Count; ++i)
            {
                Destroy(listResertIcon[i].gameObject);
            }
            if(pivotDelete)
            {
                pivotDelete = false;
                Destroy(pivot);
            }
            listResertIcon.Clear();
        }

        /// <summary>
        /// 두번의 아이콘이 없어질 아이콘을 다 체크 하였을경우 실행하는 함수.
        /// </summary>
        public void EndCheck()
        {
            if (listEraserIcon.Count < 2) return;
            int l = listEraserIcon.Count;
            for(int i = 0; i < l; ++i)
            {
                listEraserIcon[i].Remove();
            }
            listEraserIcon.Clear();
            pivot.Remove();
            pivot = null;
            /// 모든 검사가 끝났으므로 지우거나 바꿀 아이콘이 있는가?
            /// 지울 아이콘이 5개 이상이다.
            /// pivot 아이콘을 유령으로 바꾸어준후 나머지는 지운다.
            /// 지울 아이콘이 4개 이다.
            /// 아이콘이 세로로 움직였는지 가로로 움직였는데 체크한다.
            /// 세로라면 pivot아이콘을 ↕ 으로 바꾸워준다. 가로라면 ↔ 으로 바꾸어 준후 나머지는 지운다.
            /// 나머지 아이콘들은 지워준다. 만약 pivot의 아이콘이 변형되었다면 지우지 않고 냅둔다.
        }
    }
}