using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DotIndicator : Indicator {
      [SerializeField] Text t_index;
      [SerializeField] Text t_label;

      public override void SetCoordinate ( Vector2 pos ) {
            t_label.text = pos.ToString();
      }

      public override void SetIndexText ( int index ) {
            t_index.text = index.ToString();
      }
}