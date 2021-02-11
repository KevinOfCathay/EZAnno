using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {
      public int index;
      public Indicator indicator = null;
      public Text number, left, top, left_percent, top_percent, label, type;

      public void Set ( int number, Vector4 canvaspos, int label = 0 ) {
            this.number.text = number.ToString();
            this.left.text = canvaspos.x.ToString();
            this.top.text = canvaspos.y.ToString();
            this.left_percent.text = canvaspos.z.ToString();
            this.top_percent.text = canvaspos.w.ToString();
            this.label.text = label.ToString();
      }

      public void Delete () {
            Destroy(this.gameObject);
      }
}
