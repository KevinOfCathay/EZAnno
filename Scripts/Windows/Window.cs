using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Window : MonoBehaviour {
      public Transform window;
      public void Open () {
            window.transform.localScale = Vector3.one;
      }

      public void Close () {
            window.transform.localScale = Vector3.zero;
      }
}
