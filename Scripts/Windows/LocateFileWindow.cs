using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocateFileWindow : Window {
      public InputField dir_input;
      public RecordManager rm;
      public DataContainer container;

      private void Awake () {
            container = new DataContainer();
            rm = FindObjectOfType<RecordManager>();
      }

      public void Confirm () {
            if ( rm != null ) { rm.LoadDataFromPath(dir_input.text); }
            Close();
      }
}