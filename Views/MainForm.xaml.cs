using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Windows;
using System.Windows.Input;
using FabParameters.Model;
using FabParameters.Core;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Linq;

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

            

            PParamComboBox.ItemsSource = ParamValue.FBDimensions(fPList);

            SParamComboBox.ItemsSource = ElemSharedParameters.ElmSharedParam(fPList); ;
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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
   

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
    }

    

    
}
