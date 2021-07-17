using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
namespace WPF_TEST_NO_MVVM
{
    public class GridHelper
    { 
        private enum Mode { row=0,col};
        private const string starregx = @"^(\d*\.)?\d+\*$";  //check  number+star
        private const string numregx = @"^(\d*\.?\d+)$";  // check number   
        private delegate GridLength GetSizeType(string tmp);
        private delegate void SetSize(Grid grid, string str, int index, GetSizeType getSizeType);
        private static GridLength  GetType(string tmp)
        {
            Regex StarCheck = new Regex(starregx);
            Regex NumCheck = new Regex(numregx);
            if(tmp=="Auto")
            {
                return GridLength.Auto;
            }
            else if (tmp == "*") return new GridLength(1, GridUnitType.Star);
            else if (StarCheck.IsMatch(tmp))
            {
                string num = tmp.Replace('*', '\0');
                return new GridLength(float.Parse(num), GridUnitType.Star);
            }
            else if (NumCheck.IsMatch(tmp)) return new GridLength(float.Parse(tmp));
            else
            {
                throw new Exception("format error");
            }
        }
        private static  void SetHeight(Grid grid ,string str ,int index, GetSizeType getSizeType)
        {
           
            grid.RowDefinitions[index].Height = getSizeType(str);
        }

        private static void SetWidth(Grid grid, string str, int index, GetSizeType getSizeType)
        {

            grid.ColumnDefinitions[index].Width = getSizeType(str);
        }
        #region Row  Definition
        public static int GetRowCount(DependencyObject obj)
        {
            return (int)obj.GetValue(RowCountProperty);
        }

        public static void SetRowCount(DependencyObject obj, int value)
        {
            obj.SetValue(RowCountProperty, value);
      
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty  RowCountProperty =
            DependencyProperty.RegisterAttached("RowCount", typeof(int), typeof(GridHelper), new PropertyMetadata(0,RowCountChanged));

        private static void RowCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Grid grid = d as Grid;
            if (d == null || (int)e.NewValue<0) return;
            else
            {
                int size = (int)e.NewValue;
                grid.RowDefinitions.Clear();
                for(int i=0;i<size;i++)
                {
                    grid.RowDefinitions.Add(new RowDefinition() { Height=new GridLength(1,GridUnitType.Star)});
                }
            }
           
            
        }
        #endregion

        #region Column Definition


        public static int GetColumnCount(DependencyObject obj)
        {
            return (int)obj.GetValue(ColumnCountProperty);
        }

        public static void SetColumnCount(DependencyObject obj, int value)
        {
            obj.SetValue(ColumnCountProperty, value);
        }

        // Using a DependencyProperty as the backing store for ColumnCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColumnCountProperty =
            DependencyProperty.RegisterAttached("ColumnCount", typeof(int), typeof(GridHelper), new PropertyMetadata(0, ColumnCountChanged));

        private static  void ColumnCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Grid grid = d as Grid;
            if (grid == null ||(int)e.NewValue < 0) return;
            else
            {
                grid.ColumnDefinitions.Clear();
                int size = (int)e.NewValue;
                for(int i=0;i<size;i++)
                {
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                }
            }
      
        }
        #endregion
    
        #region SetRowsHeight


        public static string GetRowsHeight(DependencyObject obj)
        {
            return (string)obj.GetValue(RowsHeightProperty);
        }

        public static void SetRowsHeight(DependencyObject obj,  string value)
        {
            obj.SetValue(RowsHeightProperty, value);
         
        }

        // Using a DependencyProperty as the backing store for RowHeights.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowsHeightProperty =
            DependencyProperty.RegisterAttached("RowsHeight", typeof(string), typeof(GridHelper), new PropertyMetadata(string.Empty,RowsHeightChanged));

        private static void RowsHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Grid grid = d as Grid;
            string height = e.NewValue as string;
            if (grid == null || height == null) return;
            else
            {
             
                string [] arr =height.Split(',');
                SetSize set = new SetSize(SetHeight );
                GetSizeType getSize = new GetSizeType(GetType);

                if (arr.Length != grid.RowDefinitions.Count) throw new Exception("RowsCount amount not equal RowsHeight");
                for(int i=0;i <grid.RowDefinitions.Count;i++)
                {
                    set(grid, arr[i], i, getSize);
                }

            }
            
        }
        #endregion

        #region SetColsWidth


        public static string GetColumnsWidth(DependencyObject obj)
        {
            return (string)obj.GetValue(ColumnsWidthProperty);
        }

        public static void SetColumnsWidth(DependencyObject obj, string value)
        {
            obj.SetValue(ColumnsWidthProperty, value);
        }

        // Using a DependencyProperty as the backing store for ColumnsWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColumnsWidthProperty =
            DependencyProperty.RegisterAttached("ColumnsWidth", typeof(string), typeof(GridHelper), new PropertyMetadata(string.Empty,ColumnsWidthChanged));

        private static void ColumnsWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Grid grid = d as Grid;
            string height = e.NewValue as string;
            if (grid == null || height == null) return;
            else
            {

                string[] arr = height.Split(',');
                SetSize set = new SetSize(SetWidth);
                GetSizeType getSize = new GetSizeType(GetType);

                if (arr.Length != grid.ColumnDefinitions.Count) throw new Exception("ColumnCount amount not equal   ColumnsHeight");
                for (int i = 0; i < grid.ColumnDefinitions.Count; i++)
                {
                    set(grid, arr[i], i, getSize);
                }

            }
        }


        #endregion
    }

}
