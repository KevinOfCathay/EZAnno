using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CreateDotAnnoAndItem : IOperation {
      public Indicator indicator;
      public Item item;

      public void Set () {
            throw new NotImplementedException();
      }

      public void Perform ( AnnotationManager am, Vector4 annopos ) {

      }

      public void Undo ( AnnotationManager am ) {
            UnityEngine.Object.Destroy(indicator);
            UnityEngine.Object.Destroy(item);
      }

      public void Finished ( AnnotationManager am ) {
      }
}
