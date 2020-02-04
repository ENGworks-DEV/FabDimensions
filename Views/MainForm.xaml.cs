using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Windows;
using System.Windows.Input;
using FabParameters.Model;
using FabParameters.Core;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Linq;
using System.Windows.Media;

namespace FabParameters
{
    /// <summary>
    /// Interaction logic for MainForm.xaml
    /// </summary>
    public partial class MainForm : Window
    {

        public static UIApplication uiapp { get; set; }

        public static UIDocument uidoc { get; set; }

        public static Document doc { get; set; }

        public static List<Element> fPList = new List<Element>();



        /// <summary>
        /// Check if the suffix is a number or not
        /// </summary>
        /// <param name="cmddata_p"></param>
        public MainForm(ExternalCommandData cmddata_p)
        {
            this.DataContext = this;
            uidoc = cmddata_p.Application.ActiveUIDocument;
            doc = uidoc.Document;

            InitializeComponent();

            fPList = ViewFPElm.ViewFabParts(doc, doc.ActiveView.Id);

            if(fPList.Count != 0)
            { 

            PParamComboBox.ItemsSource = GetAllFabParam.AllParams(fPList, doc);

                SParamComboBox.ItemsSource = ViewSharedParameters.ElmSharedParam(fPList);
            }
        }


        public string projectName = App.NameSpaceNm;
        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }

        public string projectVersion = CommonAssemblyInfo.Number;
        public string ProjectVersion
        {
            get { return projectVersion; }
            set { projectVersion = value; }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)

        {
            this.DragMove();
        }

        /// <summary>
        /// Button close form when clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void Title_Link(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://google.com");
        }

        private void ComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IEnumerable<System.Windows.Controls.TextBox> TextboxList= FindVisualChildren<System.Windows.Controls.TextBox>(this);
            System.Windows.Controls.TextBox tBox = TextboxList.First();
            tBox.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void ComboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IEnumerable<System.Windows.Controls.TextBox> TextboxList = FindVisualChildren<System.Windows.Controls.TextBox>(this);
            System.Windows.Controls.TextBox tBox = TextboxList.Last();
            tBox.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void Click_Button(object sender, RoutedEventArgs e)
        {
            if (PParamComboBox.Text == "" || PParamComboBox.Text == "1. Select input parameter"
                || SParamComboBox.Text == "" || SParamComboBox.Text == "2. Select output parameter")
            {
                TaskDialog.Show("Warning", "One of the inputs is empty");
            }
            else
            {
                SetParamByName.SetParam(doc, fPList, PParamComboBox.Text, SParamComboBox.Text);
            }
        }


        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

    }




}
