using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;


namespace FabParameters.Model
{
    class ViewFPElm
    {
        public static List<Element> ViewFabParts(Document doc, ElementId view)
        {
                   
            //Get list of all Fabrication parts elements in current view
            List<Element> FabPartsElm = new FilteredElementCollector(doc, view)
                .OfCategory(BuiltInCategory.OST_FabricationDuctwork).ToList<Element>();

            return FabPartsElm;
        }    
    }
}
