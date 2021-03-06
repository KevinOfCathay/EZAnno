﻿using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
public class Dot : Annotation {
      public Dot () { type = Type.Dot; }

      public override void CreateItem () {
            throw new NotImplementedException();
      }

      public override Texture2D SetPixels ( Texture2D texture, Color color, int radius ) {
            foreach ( var point in pos ) {
                  texture.SetPixel(( int ) point.x, ( int ) point.y, Color.black);
            }
            return texture;
      }
}