using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

// Controls all the logic & events
public class AnnotationManager : MonoBehaviour {
      [SerializeField] Camera camera;
      public byte cur_label = 0;
      public Color cur_color;

      [SerializeField] DotIndicator dot_prefab;
      [SerializeField] LineIndicator line_prefab;

      /// <summary>
      /// The click switch is used when mode = Line Mode/Rect Mode
      /// Require user to click twice
      /// </summary>
      bool clickswitch = false;
      Vector3[] line_anchors = new Vector3[2];
      public RectTransform canvas;
      public ImageWindow iw;
      public RecordManager rm;
      public ItemManager im;
      public StatusWindow status;
      public ColorPicker[] color_pickers;
      Queue<IOperation> current_operation = new Queue<IOperation>(capacity: 20);
      LineIndicator current_line_indicator = null;

      List<GameObject> indicators;

      private void Awake () {
            indicators = new List<GameObject>(capacity: 25);
            foreach ( var cp in color_pickers ) {
                  cp.onSelected += Change_Label;
            }
            // Set to dot anno mode
            DotAnnoMode();

            // Event ++ onNextImage 
            // 移除画面上的所有点
            iw.onSwitchImage += () => {
                  foreach ( var obj in indicators ) { Destroy(obj); }
                  indicators.Clear();
            };

            // Event ++ onNextImage 
            // 更新index文本
            iw.onImageSwitched += rm.Update_Index_Text;
            iw.onSwitchImage += rm.SaveCurrentRecord;

            // Event ++ onDatacontainerLoaded 
            // Load image to the Canvas
            rm.onDatacontainerLoaded += ( dc ) => { iw.LoadImageBy(1); };
      }


      public void DotAnnoMode () {
            iw.ResetEvents();
            iw.onClicked += ( pointer_pos ) => {
                  Debug.Log(iw.last_click_canvas_localpos);
                  var p = CreateDotIndicator(iw.last_click_canvas_localpos, GetCurrentColor());
                  CreateDotAnnotation(iw.last_click_annopos);
                  var i = im.CreateListItem(iw.last_click_annopos);
                  i.indicator = p;
            };
            status.AddAndShowNotification("Now you're using dot mode");
      }

      public void LineAnnoMode () {
            iw.ResetEvents();
            iw.onClicked += ( pointer_pos ) => {
                  if ( !clickswitch ) {
                        // Create a new line
                        var canvas_localpoint = iw.transform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(new Vector3(pointer_pos.x, pointer_pos.y, 10f)));
                        line_anchors[0] = Camera.main.ScreenToWorldPoint(new Vector3(pointer_pos.x, pointer_pos.y, 10f));
                        Debug.Log(canvas_localpoint);
                        clickswitch = true;
                  }
                  else {
                        var canvas_localpoint = iw.transform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(new Vector3(pointer_pos.x, pointer_pos.y, 10f)));
                        line_anchors[1] = Camera.main.ScreenToWorldPoint(new Vector3(pointer_pos.x, pointer_pos.y, 10f));
                        Debug.Log(canvas_localpoint);
                        CreateLineIndicator(line_anchors, GetCurrentColor());
                        clickswitch = false;
                  }
            };
            status.AddAndShowNotification("Now you're using line mode");
      }

      public void RectAnnoMode () {
            iw.ResetEvents();
            iw.onClicked += ( point_pos ) => { };
      }

      public void Change_Label ( ColorPicker picker ) {
            cur_label = picker.label_index;
            cur_color = picker.color;
      }

      /// <summary>
      /// 创建点标识物
      /// </summary>
      public DotIndicator CreateDotIndicator ( Vector2 canvaspos, Color color ) {
            var new_dot = Instantiate(dot_prefab, parent: iw.transform);
            new_dot.transform.localScale = new Vector3(1f, 1f, 1f);
            new_dot.transform.localPosition = canvaspos;
            new_dot.SetIndexText(indicators.Count);
            new_dot.GetComponent<RawImage>().color = color;
            indicators.Add(new_dot.gameObject);
            return new_dot;
      }

      public LineIndicator CreateLineIndicator ( Vector3[] worldpos, Color color ) {
            var new_line = Instantiate(line_prefab);
            new_line.transform.localScale = new Vector3(1f, 1f, 1f);
            new_line.SetIndexText(indicators.Count);
            new_line.GetComponent<LineRenderer>().SetPositions(worldpos);
            indicators.Add(new_line.gameObject);
            return new_line;
      }

      public void CreateDotAnnotation ( Vector4 canvaspos ) {
            Dot dot = new Dot();
            dot.AddRecords(new List<Vector4> { canvaspos }, label: cur_label);
            rm.AddDotToRecord(dot);
            status.AddAndShowNotification(string.Join(" ", "Annotation created:", canvaspos.ToString()));
      }

      public void OpenWindow ( Window window ) {
            window?.Open();
      }

      private void Update () {
            if ( Input.GetKeyDown(KeyCode.Alpha0) ) {
                  Change_Label(color_pickers[0]);
            }
            else if ( Input.GetKeyDown(KeyCode.Alpha1) ) {
                  Change_Label(color_pickers[1]);
            }
            else if ( Input.GetKeyDown(KeyCode.Alpha2) ) {
                  Change_Label(color_pickers[2]);
            }
            else if ( Input.GetKeyDown(KeyCode.Alpha3) ) {
                  Change_Label(color_pickers[3]);
            }
            else if ( Input.GetKeyDown(KeyCode.Alpha4) ) {
                  Change_Label(color_pickers[4]);
            }
            else if ( Input.GetKeyDown(KeyCode.Alpha5) ) {
                  Change_Label(color_pickers[5]);
            }
            else if ( Input.GetKeyDown(KeyCode.Alpha6) ) {
                  Change_Label(color_pickers[6]);
            }

            if ( Input.GetKeyDown(KeyCode.L) ) {
                  LineAnnoMode();
            }
            else if ( Input.GetKeyDown(KeyCode.D) ) {
                  DotAnnoMode();
            }

            if ( Input.GetKey(KeyCode.LeftControl) ) {
                  if ( Input.GetKeyDown(KeyCode.Z) ) {
                        try { current_operation.Dequeue()?.Undo(this); }
                        catch ( Exception ) { }
                  }
            }
      }

      public Color GetCurrentColor () {
            return color_pickers[cur_label].color;
      }
}