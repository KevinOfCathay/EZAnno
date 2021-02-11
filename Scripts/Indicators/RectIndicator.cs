using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RectIndicator : Indicator {
      [SerializeField] Text t_index;
      [SerializeField] TextMesh t_label;
      public override void SetIndexText ( int index ) {
            t_index.text = index.ToString();
      }
      public override void SetCoordinate ( Vector2 pos ) {
            t_label.text = pos.ToString();
      }
}