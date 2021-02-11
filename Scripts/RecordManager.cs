using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Record Manager:
/// <para>Add record to records. Save and load records.</para>
/// </summary>
public class RecordManager : MonoBehaviour {
      [SerializeField] string data_dir = string.Empty;
      public DataContainer data;

      public ImageThumbnailWindow thumbnail_window;
      public Record current_record;

      public StatusWindow status;
      public int cur_record_index = -1;
      public Text t_index, t_file_name;

      public event Action<DataContainer> onDatacontainerLoaded;
      public event Action<Record> onRecordLoaded;

      private void Awake () {
            data = new DataContainer();
            onDatacontainerLoaded += ( dc ) => {
                  // Reset Index and text
                  cur_record_index = -1;
                  t_index.text = cur_record_index.ToString();

                  int i = 0;
                  foreach ( var rawimg in thumbnail_window.thumbnails ) {
                        if ( i < dc.filelist.Count ) { rawimg.texture = ImageWindow.LoadTextureFromFile(dc.filelist[i]); }
                        else { rawimg.texture = null; }
                        i += 1;
                  }
                  thumbnail_window.slider.maxValue = dc.filelist.Count - 1;
            };

            // Event ++ onSlide
            // 滑动预览窗口时触发的事件
            thumbnail_window.onSlide += ( slider ) => {
                  int v = ( int ) slider.value;
                  foreach ( var rawimg in thumbnail_window.thumbnails ) {
                        Destroy(rawimg.texture);
                        if ( v < data.filelist.Count ) { rawimg.texture = ImageWindow.LoadTextureFromFile(data.filelist[v]); }
                        else { rawimg.texture = null; }
                        v += 1;
                  }
            };
      }

      private void Start () {
            LoadDataFromPath(data_dir);
      }

      /// <summary>
      /// 读入目标路径内的所有文件
      /// </summary>
      public void LoadDataFromPath ( string path ) {
            List<string> loading_status;
            loading_status = data.SearchSavedJson(path);
            status.AddAndShowNotification(loading_status);

            data.source_directory = path;
            loading_status = data.TryGetFilepathList(path);
            status.AddAndShowNotification(loading_status);

            onDatacontainerLoaded?.Invoke(data);
      }

      public void AddDotToRecord ( Dot dot ) {
            current_record.annotations.Add(dot);
      }

      public int GetRecordIndexByPath ( string filepath ) {
            return data.records.FindIndex(( record ) => { return record.filepath == filepath; });
      }

      public Record GetRecordByPath ( string filepath ) {
            var record = data.records.Find(( record ) => { return record.filepath == filepath; });
            if ( record != null ) {
                  onRecordLoaded?.Invoke(record);
            }
            return record;
      }

      public void SaveCurrentRecord () {
            // Check if there is an existing record in the anno file.
            var record_index = GetRecordIndexByPath(current_record.filepath);
            if ( record_index == -1 ) { data.records.Add(current_record); }
            else {
                  // If there is a record in the records-list, then overwrite it
                  data.records[record_index] = current_record;
            }
            status.AddAndShowNotification(string.Join(" ", "Record saved", ".Total records:", data.records.Count.ToString()));
      }

      public void SaveAnnotationFile () {
            // Save current record to the records
            SaveCurrentRecord();
            var json = JsonUtility.ToJson(data.records);
            StreamWriter sw = new StreamWriter(Path.Combine(data_dir, Dir.annotation_json_filename));
            sw.Write(json);
            sw.Close();
      }

      public void SaveAnnotationPicture () {
            // Save current record to the records
            SaveCurrentRecord();
            if ( !Directory.Exists(Path.Combine(data.source_directory, "pic")) ) {
                  Directory.CreateDirectory(Path.Combine(data.source_directory, "pic"));
            }

            foreach ( var record in data.records ) {
                  // Create a blank texture
                  var texture = ImageWindow.CreateBlankTexture(record.width, record.height);
                  foreach ( var annotation in record.annotations ) {
                        texture = annotation.SetPixels(texture, Color.black, radius: 1);
                  }
                  texture.Apply();
                  byte[] _bytes = texture.EncodeToPNG();

                  string path = Path.Combine(data.source_directory, "pic", record.filename + ".png");
                  System.IO.File.WriteAllBytes(path, _bytes);
            }
      }

      public void Update_Index_Text () {
            t_index.text = cur_record_index.ToString();
      }
}