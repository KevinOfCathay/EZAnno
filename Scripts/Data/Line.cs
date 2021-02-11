using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
public class Line : Annotation {
      public Line () { type = Type.Line; }

      public override void CreateItem () {
            throw new NotImplementedException();
      }

      public override Texture2D SetPixels ( Texture2D texture, Color color, int radius ) {
            throw new NotImplementedException();
      }
}
