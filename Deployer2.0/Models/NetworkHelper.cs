using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vmware.vapi.bindings;
using vmware.vcenter;

namespace Deployer2._0.Models
{
    public class NetworkHelper
    {
        public static string GetStandardNetworkBacking(
            StubFactory stubFactory, StubConfiguration sessionStubConfig,
            string datacenterName, string stdPortgroupName)
        {
            HashSet<string> datacenters = new HashSet<string>
            {
                DatacenterHelper.GetDatacenter(
                    stubFactory, sessionStubConfig, datacenterName)
            };
            NetworkTypes.FilterSpec networkFilterSpec =
                new NetworkTypes.FilterSpec();
            networkFilterSpec.SetNames(
                new HashSet<string> { stdPortgroupName });
            networkFilterSpec.SetDatacenters(datacenters);
            networkFilterSpec.SetTypes(new HashSet<NetworkTypes.Type>
            {
                NetworkTypes.Type.STANDARD_PORTGROUP
            });

            Network networkService =
                stubFactory.CreateStub<Network>(sessionStubConfig);
            List<NetworkTypes.Summary> networkSummaries =
                networkService.List(networkFilterSpec);

            if (networkSummaries.Count > 1)
            {
                throw new Exception(String.Format("More than one standard " +
                    " portgroup with the specified name {0} exist",
                    stdPortgroupName));

            }
            if (networkSummaries.Count <= 0)
            {
                throw new Exception(String.Format("Standard portgroup with " +
                                    "name {0} not found !", stdPortgroupName));
            }

            return networkSummaries[0].GetNetwork();
        
        }

        public static string GetDistributedNetworkBacking(
            StubFactory stubFactory, StubConfiguration sessionStubConfig,
            string datacenterName, string distPortgroupName)
        {
            HashSet<string> datacenters = new HashSet<string>
            {
                DatacenterHelper.GetDatacenter(
                    stubFactory, sessionStubConfig, datacenterName)
            };
            NetworkTypes.FilterSpec networkFilterSpec =
                new NetworkTypes.FilterSpec();
            networkFilterSpec.SetNames(
                new HashSet<string> { distPortgroupName });
            networkFilterSpec.SetDatacenters(datacenters);
            networkFilterSpec.SetTypes(new HashSet<NetworkTypes.Type>
            {
                NetworkTypes.Type.DISTRIBUTED_PORTGROUP
            });
            Network networkService =
                stubFactory.CreateStub<Network>(sessionStubConfig);
            List<NetworkTypes.Summary> networkSummaries =
                networkService.List(networkFilterSpec);

            if (networkSummaries.Count > 1)
            {
                throw new Exception(String.Format("More than one distributed" +
                    " portgroup with the specified name {0} exist",
                    distPortgroupName));

            }

            if (networkSummaries.Count <= 0)
            {
                throw new Exception(String.Format("Distributed portgroup " +
                                    "with name {0} not found !",
                                    distPortgroupName));
            }

            return networkSummaries[0].GetNetwork();
        }
    }
}