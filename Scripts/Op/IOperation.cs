using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface IOperation {
      public void Set ();
      public void Perform ( AnnotationManager am, Vector4 annopos );
      public void Finished ( AnnotationManager am );
      public void Undo ( AnnotationManager am );
}