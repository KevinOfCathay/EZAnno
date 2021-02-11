using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CreateDotAndItem : IOperation {
      public Indicator indicator;
      public Item item;

      public void Set () {
            throw new NotImplementedException();
      }

      public void Perform ( AnnotationManager am, Vector4 annopos ) {
            indicator = am.CreateDotIndicator(am.iw.AnnotationPosToCanvasLocalpos(annopos), am.GetCurrentColor());
            item = am.im.CreateListItem(annopos);
      }

      public void Undo ( AnnotationManager am ) {
            UnityEngine.Object.Destroy(indicator);
            UnityEngine.Object.Destroy(item);
      }

      public void Finished ( AnnotationManager am ) {
      }
}
