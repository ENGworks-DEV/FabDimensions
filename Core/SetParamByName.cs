using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using FabParameters.Model;

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

                string value;



                for (var i = 0; i < elems.Count; i++)
                {
                    Parameter sharParam = elems[i].LookupParameter(sParam);

                    switch (PParam)
                    {
                        case "C1":
                            value = FPartConnValue.FBConnectors(elems[i], doc, 0);
                            if(value != "")
                            {
                                ParamAsign(sharParam, value);
                                counter++;
                            }
                            break;
                        case "C2":
                            value = FPartConnValue.FBConnectors(elems[i], doc, 1);
                            if (value != "")
                            {
                                ParamAsign(sharParam, value);
                                counter++;
                            }
                            break;
                        case "C3":
                            value = FPartConnValue.FBConnectors(elems[i], doc, 2);
                            if (value != "")
                            {
                                ParamAsign(sharParam, value);
                                counter++;
                            }
                            break;
                        case "C4":
                            value = FPartConnValue.FBConnectors(elems[i], doc, 3);
                            if (value != "")
                            {
                                ParamAsign(sharParam, value);
                                counter++;
                            }
                            break;

                        default:
                            try
                            {
                                FabricationPart fp = (FabricationPart)elems[i];

                                IList<FabricationDimensionDefinition> dimObj = fp.GetDimensions();

                                FabricationDimensionDefinition Dparam = dimObj.First(item => item.Name.ToString() == PParam);

                                value = fp.GetDimensionValue(Dparam).ToString();

                                ParamAsign(sharParam, value);

                                counter++;
                            }
                            catch
                            {

                            }
                            break;
                    }
                }

                AssingParamNumber.Commit();
            }
        }

        private static void ParamAsign(Parameter sharParam, string value)
        {
            switch (sharParam.StorageType)
            {
                case StorageType.Double:
                    double errorDouble;
                    if (double.TryParse(value, out errorDouble))
                    {
                        var v = double.Parse(value.ToString());
                        sharParam.Set(v);
                    }
                    break;
                case StorageType.Integer:
                    int errorInt;
                    if (int.TryParse(value.ToString(), out errorInt))
                    {
                        var v = int.Parse(value.ToString());
                        ElementId id = new ElementId(v);
                        sharParam.Set(id);
                    }
                    break;
                case StorageType.String:
                    sharParam.Set(value.ToString());
                    break;

                default:
                    break;
            }
        }
    }
}
