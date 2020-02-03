using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace FabParameters.Model
{
    class ParamValue
    {
        public static List<string> FBDimensions (List<Element> fPartList)
        {
            List<FabricationPart> input = new List<FabricationPart>();

            for (var i = 0; i < fPartList.Count; i++)
            {
                input.Add((FabricationPart)fPartList[i]);
            }

            List<string> result = new List<string>();

            for (var i = 0; i < input.Count; i++)
            {
                IList<FabricationDimensionDefinition> dimObj = input[i].GetDimensions();

                foreach (FabricationDimensionDefinition x in dimObj)
                {
                    if (!result.Contains(x.Name.ToString()))
                    {
                        result.Add(x.Name.ToString());
                    }                  
                }
            }

            result.Sort();
            return result;
        }
    }
}
