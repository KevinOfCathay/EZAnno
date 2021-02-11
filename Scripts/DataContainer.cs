using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataContainer {
      /// <summary>
      /// sample 数据的文件夹路径
      /// </summary>
      public string source_directory = string.Empty;
      /// <summary>
      /// Sample 数据的路径
      /// </summary>
      public List<string> filelist = new List<string>(capacity: 500);
      /// <summary>
      /// 记录在案的annotation records
      /// </summary>
      public List<Record> records = new List<Record>(capacity: 100);

      public List<string> TryGetFilepathList ( string dirpath ) {
            List<string> res = new List<string>(capacity: 1000);
            var status = new List<string>(capacity: 5);
            try {
                  if ( Directory.Exists(dirpath) ) {
                        var tempfilelist = Directory.GetFiles(dirpath);
                        int imagecount = 0;
                        foreach ( var file in tempfilelist ) {
                              string ext = Path.GetExtension(file);
                              if ( ext == ".png" || ext == ".jpg" ) {
                                    imagecount += 1;
                                    res.Add(file);
                              }
                        }
                        // Assign the paths to the file list
                        filelist = res;
                        status.Add("Get files successful.");
                        status.Add(string.Join(" ", "Total files in directory:", tempfilelist.Length.ToString(), "Total Images:", imagecount.ToString()));
                  }
                  else {
                        status.Add("Can't find the directory.");
                  }
            }
            catch ( Exception ) {
                  status.Add("Get files failed.");
                  Debug.LogWarning("TryGetFilepathList exception catched");
            }
            return status;
      }

      public List<string> SearchSavedJson ( string dirpath ) {
            List<Record> records = new List<Record>();
            List<string> status = new List<string>(capacity: 5);
            try {
                  if ( File.Exists(Path.Combine(dirpath, Dir.annotation_json_filename)) ) {
                        // Load Json File
                        StreamReader sr = new StreamReader(Path.Combine(dirpath, Dir.annotation_json_filename));
                        string json = sr.ReadToEnd();
                        records = JsonUtility.FromJson<List<Record>>(json);
                        sr.Close();
                  }
                  else {
                        status.Add("Saved annotation file not found.");
                  }
                  this.records = records;
            }
            catch ( Exception ) { }
            return status;
      }
}
