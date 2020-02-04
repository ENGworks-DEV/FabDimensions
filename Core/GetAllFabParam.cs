using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FabParameters.Model;
using Autodesk.Revit.DB;

namespace FabParameters.Core
{
    class GetAllFabParam
    {
        public static List<string> AllParams (List<Element> fPlist, Document doc)
        {
            List<string> result = new List<string>();

            List<string> FBDim = FBDimensionsValue.FBDimensions(fPlist);

            List<string> FBConn = new List<string>(new string[] { "C1", "C2", "C3", "C4" }); ;

            result = FBDim.Concat(FBConn)
                                    .ToList();
            result.Sort();
            return result;
        }

        
    }
}
