using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vmware.vapi.bindings;
using vmware.vcenter;

namespace Deployer2._0.Models
{
    public class PlacementHelper
    {
        public static VMTypes.PlacementSpec GetPlacementSpecForCluster(
            StubFactory stubFactory, StubConfiguration sessionStubConfig,
            string datacenterName, string clusterName, string folderName,
            string datastoreName)
        {


            string clusterId =
                    ClusterHelper.GetCluster(stubFactory, sessionStubConfig,
                    datacenterName, clusterName);
            Console.WriteLine("Selecting cluster " + clusterName + "(id=" +
                              clusterId + ")");

            string folderId = FolderHelper.GetFolder(stubFactory,
                sessionStubConfig, datacenterName, folderName);
            Console.WriteLine("Selecting folder " + folderName + "(id=" +
                              folderId + ")");

            string datastoreId =
            DatastoreHelper.GetDatastore(stubFactory, sessionStubConfig,
            datacenterName, datastoreName);
            Console.WriteLine("Selecting datastore " + datastoreName + "(id=" +
                              datastoreId + ")");

            
            VMTypes.PlacementSpec vmPlacementSpec =
                new VMTypes.PlacementSpec();
            vmPlacementSpec.SetDatastore(datastoreId);
            vmPlacementSpec.SetCluster(clusterId);
            vmPlacementSpec.SetFolder(folderId);

            return vmPlacementSpec;
        }
    }
}