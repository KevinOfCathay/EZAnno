using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  一个单一的Annotation，
///  包括坐标、Anno类型、标签...
/// </summary>
[Serializable]
public abstract class Annotation {
      public enum Type { Dot, Line, Rect };
      public byte label;
      public Type type;
      public List<Vector4> pos = new List<Vector4>(capacity: 4);
      public void AddRecords ( List<Vector4> records, byte label ) {
            if ( records != null ) {
                  pos.AddRange(records);
            }
            this.label = label;
      }
      public abstract void CreateItem ();
      public abstract Texture2D SetPixels ( Texture2D texture, Color color, int radius );
}