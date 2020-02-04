using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace FabParameters.Model
{
    class ViewSharedParameters
    {
        //Get all the existing elements shared parameters in the view
        public static List<string> ElmSharedParam(List<Element> elems)
        {
            List<string> result = new List<string>();

            ParameterSet elemParams = elems[0].Parameters;

            foreach(Parameter i in elemParams)
            {
                if(i.IsShared == true)
                {
                    result.Add(i.Definition.Name.ToString());
                }
            }
            result.Sort();

            return result;
        }
    }
}
