using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace FabParameters.Model
{
    class FPartConnValue
    {
            public static string FBConnectors(Element fPartList, Document doc, int order)
            {

                FabricationConfiguration config = FabricationConfiguration.GetFabricationConfiguration(doc);

                FabricationPart input = (FabricationPart)fPartList;

                ConnectorManager temConnMan = input.ConnectorManager;

                ConnectorSet tempConnectSet = temConnMan.Connectors;

                List<Connector> listConn = new List<Connector>();

                foreach (Connector e in tempConnectSet)
                    {
                        listConn.Add(e);
                    }
            

                List<int> ElmInt = new List<int>();

                for (var i = 0; i < listConn.Count; i++)
                {
                    try{ 
                    ElmInt.Add(listConn[i].GetFabricationConnectorInfo().BodyConnectorId);
                    }
                    catch
                    {

                    }
                }

                List<string> result = new List<string>();

                for (var i = 0; i < ElmInt.Count; i++)
                {
                    result.Add(config.GetFabricationConnectorGroup(ElmInt[i]).ToString());
                }

                try
                {
                return result[order];
                }
                catch
                {
                return "";
                }
                
            }
    }
}
