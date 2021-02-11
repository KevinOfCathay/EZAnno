using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ImageThumbnailWindow : MonoBehaviour {
      public RawImage[] thumbnails;
      public Slider slider;
      public event Action<Slider> onSlide;

      public void onSliderSlide () {
            if ( onSlide != null ) { onSlide.Invoke(slider); }
      }
}
