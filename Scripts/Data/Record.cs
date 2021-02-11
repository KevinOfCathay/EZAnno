using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Record: 单个数据下的所有注释记录，
/// 包含了所有的Annotation
/// </summary>
[Serializable]
public class Record {
      /// <summary>
      /// 图片数据的路径，文件名
      /// </summary>
      public string filepath, filename;
      /// <summary>
      /// 图片数据的尺寸
      /// </summary>
      public int width, height;

      public List<Label> labels = new List<Label>(capacity: 10);
      public List<Annotation> annotations = new List<Annotation>(capacity: 100);

      /// <summary>
      /// 该数据的额外注释
      /// </summary>
      public string notes;
}