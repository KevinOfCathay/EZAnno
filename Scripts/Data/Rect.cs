using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Rect : Annotation {
      public Rect () { type = Type.Rect; }

      public override void CreateItem () {
            throw new NotImplementedException();
      }

      public override Texture2D SetPixels ( Texture2D texture, Color color, int radius ) {
            throw new NotImplementedException();
      }
}
