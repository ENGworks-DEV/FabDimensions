
using System.Collections.Generic;
using Autodesk.Revit.DB;


namespace FabParameters.Model
{
    class FBDimensionsValue
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
