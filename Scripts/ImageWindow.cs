using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageWindow : MonoBehaviour, IPointerDownHandler {
      public AnnotationManager am;
      public RecordManager rm;

      public RawImage rawimg;
      [SerializeField] float width, height, wscale, hscale;
      [SerializeField] Toggle hasrecord;
      [SerializeField] RectTransform canvas;
      [SerializeField] Vector3 imgpos;
      float max_length = 860f;

      public Vector2 last_click_mouse_position { get; private set; }
      public Vector2 last_click_canvas_localpos { get; private set; }
      public Vector4 last_click_annopos { get; private set; }

      public event Action onSwitchImage, onImageSwitched;
      public event Action onLoadImage;
      public event Action<Texture2D, RawImage> onTextureLoaded;
      public event Action<Vector2> onClicked;

      private void Awake () {
            // Get the center point
            imgpos = GetComponent<RectTransform>().anchoredPosition;
            Debug.Log(string.Join(" ", "Canvas created at position:", transform.position,
                  "Rect Position", GetComponent<RectTransform>().anchoredPosition, "local position:", transform.localPosition));
      }

      /// <summary> When user click the canvas </summary>
      public void OnPointerDown ( PointerEventData eventData ) {
            last_click_mouse_position = eventData.position;
            last_click_canvas_localpos = transform.InverseTransformPoint(
                  Camera.main.ScreenToWorldPoint(new Vector3(last_click_mouse_position.x, last_click_mouse_position.y, 10f)));
            last_click_annopos = CanvasLocalposToAnnotationPos(last_click_canvas_localpos);
            if ( onClicked != null ) { onClicked.Invoke(eventData.position); }
      }

      public void ResetEvents () {
            onClicked = null;
      }

      /// <summary>
      /// 重新调整画布的大小，使得图像的长边保持最大尺寸 [1000f]，短边随着长边缩放
      /// e.g. 200 * 100 的图像 --> 1000f * 500f的画布
      /// </summary>
      public void Resize ( float imgwidth, float imgheight ) {
            if ( imgheight >= imgwidth ) {
                  width = imgwidth / (imgheight / max_length);
                  height = max_length;
                  rawimg.rectTransform.sizeDelta = new Vector2(width, height);
            }
            else if ( imgwidth > imgheight ) {
                  width = max_length;
                  height = imgheight / (imgwidth / max_length);
                  rawimg.rectTransform.sizeDelta = new Vector2(max_length, imgheight / (imgwidth / max_length));
            }
      }

      /// <summary>
      /// 将画布下的2D local position转换为图片的坐标（左、上、左百分比、上百分比）
      /// </summary>
      public Vector4 CanvasLocalposToAnnotationPos ( Vector2 canvaslocalpos ) {
            float left = (canvaslocalpos.x + width / 2) * wscale;
            float top = (height / 2 - canvaslocalpos.y) * hscale;
            float left_percent = (canvaslocalpos.x + width / 2) / width;
            float top_percent = (height / 2 - canvaslocalpos.y) / height;
            return new Vector4(left, top, left_percent, top_percent);
      }

      /// <summary>
      /// 将图片的坐标（左、上、左百分比、上百分比）转换为画布下的2D local position
      /// </summary>
      public Vector2 AnnotationPosToCanvasLocalpos ( Vector4 annotationpos ) {
            return new Vector2(annotationpos.x / wscale - width / 2, height / 2 - annotationpos.y / hscale);
      }

      public static Texture2D LoadTextureFromFile ( string filepath ) {
            Texture2D tex = new Texture2D(1, 1);      // Texture size does not matter
            var bytes = File.ReadAllBytes(filepath);
            ImageConversion.LoadImage(tex, bytes);
            return tex;
      }

      public static Texture2D CreateBlankTexture ( int width, int height ) {
            Texture2D texture = new Texture2D(width, height);
            return texture;
      }

      /// <summary>
      /// 获取距离当前X个单位的图片
      /// </summary>
      public void LoadImageBy ( int shift ) {
            if ( onSwitchImage != null ) { onSwitchImage.Invoke(); }
            if ( rm.data.filelist.Count > 0 && rm.cur_record_index + shift >= 0 && rm.cur_record_index + shift < rm.data.filelist.Count ) {
                  if ( onLoadImage != null ) { onLoadImage.Invoke(); }
                  // Shift the index pointer
                  rm.cur_record_index += shift;

                  // Get next image file's path
                  string filepath = rm.data.filelist[rm.cur_record_index];
                  rm.t_file_name.text = Path.GetFileName(filepath);

                  var tex = ImageWindow.LoadTextureFromFile(filepath);
                  onTextureLoaded.Invoke(tex, rawimg);

                  var width = tex.width; var height = tex.height;
                  rawimg.texture = tex;
                  Resize(width, height);

                  wscale = tex.width / rawimg.rectTransform.sizeDelta.x;
                  hscale = tex.height / rawimg.rectTransform.sizeDelta.y;

                  var record = rm.GetRecordByPath(filepath);

                  // If there is a record in the json file. Then load it. 
                  // Otherwise, create a brand new record.
                  if ( record != null ) {
                        rm.current_record = record;
                        hasrecord.isOn = true;
                        rm.status.AddAndShowNotification(string.Join(" ", "File", filepath, "has an existing record."));
                        foreach ( var annotation in record.annotations ) {
                              CreateDotAndItem op = new CreateDotAndItem();
                              op.Perform(am, annotation.pos[0]);
                        }
                  }
                  else {
                        hasrecord.isOn = false;
                        rm.current_record = new Record {
                              filepath = rm.data.filelist[rm.cur_record_index],
                              filename = Path.GetFileName(filepath),
                              width = tex.width,
                              height = tex.height
                        };
                  }
            }
            if ( onImageSwitched != null ) { onImageSwitched.Invoke(); }
      }

}