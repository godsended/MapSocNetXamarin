using CustomRenderer.iOS;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Editor), typeof(CustomEditorRenderer))]
namespace CustomRenderer.iOS
{
    public class CustomEditorRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {

                
                //Control.Layer.CornerRadius = 10;
                //Control.TextColor = UIColor.White;

            }
        }
    }
}