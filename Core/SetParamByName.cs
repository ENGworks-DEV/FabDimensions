using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace FabParameters.Core
{
    class SetParamByName
    {
        public static void SetParam(Document doc, List<Element> elems, string PParam, string sParam)
        {

            //ParameterSet elemParams = elems[0].Parameters;

            //GlobalParameter sParameter;

            //foreach (Parameter i in elemParams)
            //{
            //    if (i.Definition.Name.ToString() == sParam)
            //    {
            //        sParameter = doc.GetElement(i.Id) as GlobalParameter;
            //    }
            //}
            int ctSucess = 0;

            using (Autodesk.Revit.DB.Transaction AssingParamNumber = new Autodesk.Revit.DB.Transaction(doc, "Assing Part Number"))
            {
                AssingParamNumber.Start();

                for (var i = 0; i < elems.Count; i++)
                {
                    try
                    {
                        FabricationPart fp = (FabricationPart)elems[i];

                        IList<FabricationDimensionDefinition> dimObj = fp.GetDimensions();

                        foreach (FabricationDimensionDefinition x in dimObj)
                        {
                            if (x.Name.ToString() == PParam)
                            {
                                elems[i].LookupParameter(sParam).Set(fp.GetDimensionValue(x).ToString());
                                ctSucess++;
                            }
                        }
                    }
                    catch
                    {

                    }
                }

                AssingParamNumber.Commit();

                TaskDialog.Show("succesful", ctSucess.ToString() + " Elements where succesfully overwritten");

            }
        }

    }
}
