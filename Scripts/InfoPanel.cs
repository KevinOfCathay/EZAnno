using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour {
      [SerializeField] Transform info_area;
      [SerializeField] Text t_width, t_height, t_wzoom, t_hzoom;
      [SerializeField] Text t_version_number;
      [SerializeField] Text t_total_files;
      bool areaon = true;

      private void Awake () {
            var window = FindObjectOfType<ImageWindow>();
            if ( window != null ) {
                  window.onTextureLoaded += ( tex, rawimg ) => {
                        var wscale = tex.width / rawimg.rectTransform.sizeDelta.x;
                        var hscale = tex.height / rawimg.rectTransform.sizeDelta.y;
                        t_width.text = tex.width.ToString(); t_height.text = tex.height.ToString();
                        t_wzoom.text = wscale.ToString(); t_hzoom.text = hscale.ToString();
                  };
            }
            t_version_number.text = Application.version;

            var rm = FindObjectOfType<RecordManager>();
            if ( rm != null ) {
                  rm.onDatacontainerLoaded += ( dc ) => {
                        t_total_files.text = dc.filelist.Count.ToString();
                  };
            }
      }

      public void InfoSwitch () {
            if ( areaon ) {
                  info_area.transform.localScale = Vector3.zero;
                  areaon = false;
            }
            else {
                  info_area.transform.localScale = Vector3.one;
                  areaon = true;
            }
      }
}
