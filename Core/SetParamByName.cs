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

            int ctSucess = 0;
            ParamFind(ref ctSucess, doc, elems, PParam, sParam);

            TaskDialog.Show("succesful", ctSucess.ToString()
                + " Elements where succesfully overwritten");
        }


        private static void ParamFind(ref int counter, Document doc, 
            List<Element> elems, string PParam, string sParam)
        {
            using (Autodesk.Revit.DB.Transaction AssingParamNumber = 
                new Autodesk.Revit.DB.Transaction(doc, "Assing Part Number"))
            {
                AssingParamNumber.Start();

                for (var i = 0; i < elems.Count; i++)
                {
                    try
                    {
                        FabricationPart fp = (FabricationPart)elems[i];

                        IList<FabricationDimensionDefinition> dimObj = fp.GetDimensions();

                        FabricationDimensionDefinition Dparam = dimObj.First(item => item.Name.ToString() == PParam);

                        Parameter sharParam = elems[i].LookupParameter(sParam);

                            switch (sharParam.StorageType)
                            {
                                case StorageType.Double:
                                    double errorDouble;
                                    if (double.TryParse(fp.GetDimensionValue(Dparam).ToString(), out errorDouble))
                                    {
                                            var v = double.Parse(fp.GetDimensionValue(Dparam).ToString());
                                            sharParam.Set(v);
                                    }
                                    break;
                                case StorageType.Integer:
                                        int errorInt;
                                        if (int.TryParse(fp.GetDimensionValue(Dparam).ToString(), out errorInt))
                                        {
                                            var v = int.Parse(fp.GetDimensionValue(Dparam).ToString());
                                            ElementId id = new ElementId(v);
                                            sharParam.Set(id);
                                        }
                                        break;
                                case StorageType.String:
                                        sharParam.Set(fp.GetDimensionValue(Dparam).ToString());
                                        break;

                                    default:
                                        break;
                                }
                                counter++;          
                    }
                    catch
                    {

                    }
                }

                AssingParamNumber.Commit();
            }
        }

        private static void ParamAsign()
        {

        }
    }
}
