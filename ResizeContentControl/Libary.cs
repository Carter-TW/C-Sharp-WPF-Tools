using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows;
namespace Libary
{
    public class MoveThumb : Thumb
    {
        public MoveThumb()
        {
            DragDelta += OnDragDelta;
        }

        private void OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            Control designerItem = this.DataContext as Control;
            double xadjust = Canvas.GetLeft(designerItem) + e.HorizontalChange;
            double yadjust = Canvas.GetTop(designerItem) + e.VerticalChange;

            if (xadjust > 0 && yadjust > 0)
            {
                Canvas.SetLeft(designerItem, xadjust);
                Canvas.SetTop(designerItem, yadjust);
            }
        }
    }
    public class ResizeThumb : Thumb
    {
        public ResizeThumb()
        {
            DragDelta += OnDragDelta;
        }

        private void OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            ResizeThumb resize = sender as ResizeThumb;
            Control designerItem = this.DataContext as Control;


            //      
           
            switch (VerticalAlignment)
            {
                case VerticalAlignment.Bottom:
                    if (designerItem.Height + e.VerticalChange < 0) return;
                    designerItem.Height += e.VerticalChange;
                    break;
                case VerticalAlignment.Top:
                    if (designerItem.Height - e.VerticalChange < 0) return;
                    designerItem.Height -= e.VerticalChange;
                    Canvas.SetTop(designerItem, Canvas.GetTop(designerItem) + e.VerticalChange);
                    break;
                default:
                    break;
            }

            switch (HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    if (designerItem.Width - e.HorizontalChange < 0) return;
                    designerItem.Width -= e.HorizontalChange;
                    Canvas.SetLeft(designerItem, Canvas.GetLeft(designerItem) + e.HorizontalChange);
                    break;
                case HorizontalAlignment.Right:
                    if (designerItem.Width + e.HorizontalChange < 0) return;
                    designerItem.Width += e.HorizontalChange;
                    break;
                default:
                    break;
            }

        }
    }
}
