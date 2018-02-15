using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vmware.vapi.bindings;
using vmware.vcenter;

namespace Deployer2._0.Models
{
    public class ClusterHelper
    {
        public static String GetCluster(
            StubFactory stubFactory, StubConfiguration sessionStubConfig,
            string datacenterName, string clusterName)
        {

            HashSet<string> datacenters = new HashSet<string>
            {
                DatacenterHelper.GetDatacenter(stubFactory, sessionStubConfig,
                datacenterName)
            };

            ClusterTypes.FilterSpec clusterFilterSpec =
                new ClusterTypes.FilterSpec();
            HashSet<string> clusters = new HashSet<string> { clusterName };

            clusterFilterSpec.SetNames(clusters);
            clusterFilterSpec.SetDatacenters(datacenters);

            Cluster clusterService = stubFactory.CreateStub<Cluster>(
                sessionStubConfig);
            List<ClusterTypes.Summary> clusterSummaries =
                clusterService.List(clusterFilterSpec);

            if (clusterSummaries.Count > 1)
            {
                throw new Exception(String.Format("More than one cluster with"
                    + " the specified name {0} exist", clusterName));
            }

            if (clusterSummaries.Count <= 0)
            {
                throw new Exception(String.Format("Cluster with name {0}" +
                                    " not found !", clusterName));
            }

            return clusterSummaries[0].GetCluster();
        }
    }
}