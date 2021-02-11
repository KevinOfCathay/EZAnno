using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class Indicator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
      [SerializeField] protected Transform label;
      public void OnPointerEnter ( PointerEventData eventData ) {
            this.transform.DOScale(1.25f, 0.4f);
            label.localScale = Vector3.one;
      }

      public void OnPointerExit ( PointerEventData eventData ) {
            this.transform.DOScale(1f, 0.4f);
            label.localScale = Vector3.zero;
      }

      /// <summary>
      /// 更新indicator的index文本
      /// </summary>
      public abstract void SetIndexText ( int index );
      public abstract void SetCoordinate ( Vector2 pos );
}
