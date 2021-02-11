using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CreateAnnoItem : IOperation {
      public Item item;

      public void Perform ( AnnotationManager am, Vector4 annopos ) {
            throw new NotImplementedException();
      }

      public void Set () {
            throw new NotImplementedException();
      }

      public void Undo ( AnnotationManager am ) {
            throw new NotImplementedException();
      }

      public void Finished ( AnnotationManager am ) {
      }
}
