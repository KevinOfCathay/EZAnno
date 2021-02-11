using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusWindow : MonoBehaviour {
      public TextMeshProUGUI status;
      private List<string> notification = new List<string>(capacity: 50);
      private int cur_index;

      public void AddAndShowNotification ( string text ) {
            status.text = text + '\n';
            notification.Add(text);
            cur_index = notification.Count - 1;
      }

      public void AddAndShowNotification ( IEnumerable<string> text ) {
            foreach ( var t in text ) {
                  status.text = t + '\n';
                  notification.Add(t);
                  cur_index = notification.Count - 1;
            }
      }

      public void AddAndAppendNotification ( string text ) {
            status.text += text + '\n';
            notification.Add(text);
            cur_index = notification.Count - 1;
      }

      public void AddAndAppendNotification ( IEnumerable<string> text ) {
            foreach ( var t in text ) {
                  status.text += t + '\n';
                  notification.Add(t);
                  cur_index = notification.Count - 1;
            }
      }

      public void ShowPreviousNotification () {
            if ( cur_index > 0 ) {
                  cur_index -= 1;
                  status.text = notification[cur_index];
            }
      }

      public void ShowNextNotification () {
            if ( cur_index < notification.Count - 1 ) {
                  cur_index += 1;
                  status.text = notification[cur_index];
            }
      }
}

