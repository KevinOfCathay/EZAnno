using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemManager : MonoBehaviour {
      private List<Item> items;
      [SerializeField] AnnotationManager am;
      [SerializeField] GameObject tempitem;
      public Transform itemlist;
      private Vector3 initial_itempos;
      public Slider slider;

      private void Awake () {
            items = new List<Item>(capacity: 20);
            var iw = FindObjectOfType<ImageWindow>();

            // Event ++ onNextImage 
            // 清空清单
            if ( iw != null ) { iw.onSwitchImage += () => { ClearItems(); }; }
      }

      private void Start () {
            initial_itempos = itemlist.localPosition;
      }

      public Item CreateListItem ( Vector4 canvaspos ) {
            var newitem = Instantiate(tempitem, parent: itemlist);
            newitem.transform.localScale = new Vector3(1f, 1f, 1f);
            var newitem_item = newitem.GetComponent<Item>();
            newitem_item.Set(items.Count, canvaspos, am.cur_label);
            newitem_item.index = items.Count;
            items.Add(newitem_item);
            slider.maxValue += 10f;
            return newitem_item;
      }

      public void Delete_Item ( int index ) {
            Destroy(items[index].indicator.gameObject);
            Destroy(items[index].gameObject);
            items.RemoveAt(index);
            for ( int i = index; i < items.Count; i += 1 ) {
                  items[i].index -= 1;
            }
      }

      public void Delete_Item ( Item item ) {
            int index = item.index;
            items.Remove(item);
            Destroy(item.indicator.gameObject);
            Destroy(item.gameObject);
            for ( int i = index; i < items.Count; i += 1 ) {
                  items[i].index -= 1;
            }
      }

      /// <summary> If items count > 0, then clear items </summary>
      public void ClearItems () {
            if ( items.Count > 0 ) {
                  foreach ( var item in items ) {
                        Destroy(item.gameObject);
                  }
                  items.Clear();
            }
      }

      public void Scroll_Item_List () {
            itemlist.transform.localPosition = initial_itempos + new Vector3(0f, slider.value, 0f);
      }
}
