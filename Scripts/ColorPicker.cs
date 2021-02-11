using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class ColorPicker : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
      public byte label_index;
      public Color color;
      public Gradient gradient;
      public event Action<ColorPicker> onSelected;

      private void Start () {
            this.GetComponent<Image>().color = color;
      }

      public void OnPointerClick ( PointerEventData eventData ) {
            if ( onSelected != null ) { onSelected.Invoke(this); }
      }

      public void OnPointerEnter ( PointerEventData eventData ) {
            this.transform.DOScale(1.2f, 1f);
      }

      public void OnPointerExit ( PointerEventData eventData ) {
            this.transform.DOScale(1f, 1f);
      }
}