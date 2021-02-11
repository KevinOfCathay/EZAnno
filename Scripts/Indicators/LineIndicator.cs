using System.Collections;
using DG.Tweening;
using UnityEngine;

public class LineIndicator : Indicator {
      [SerializeField] TextMesh t_index;
      [SerializeField] TextMesh t_label;

      public override void SetIndexText ( int index ) {
            t_index.text = index.ToString();
      }
      public override void SetCoordinate ( Vector2 pos ) {
            t_label.text = pos.ToString();
      }
}