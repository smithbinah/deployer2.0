using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vmware.vapi.bindings;
using vmware.vcenter;

namespace Deployer2._0.Models
{
    public class DatastoreHelper
    {
        public static String GetDatastore(
            StubFactory stubFactory, StubConfiguration sessionStubConfig,
            string datacenterName, string datastoreName)
        {
            HashSet<string> datacenters = new HashSet<string>
            {
                DatacenterHelper.GetDatacenter(
                    stubFactory, sessionStubConfig, datacenterName)
            };
            DatastoreTypes.FilterSpec dsFilterSpec =
                new DatastoreTypes.FilterSpec();
            dsFilterSpec.SetNames(new HashSet<string> { datastoreName });
            dsFilterSpec.SetDatacenters(datacenters);

            Datastore datastoreService =
                stubFactory.CreateStub<Datastore>(sessionStubConfig);
            List<DatastoreTypes.Summary> dsSummaries =
                datastoreService.List(dsFilterSpec);

            if (dsSummaries.Count > 1)
            {
                throw new Exception(String.Format("More than one datastore" +
                    " with the specified name {0} exist", datastoreName));
            }

            if (dsSummaries.Count <= 0)
            {
                throw new Exception(String.Format("Datastore with name {0}" +
                                    "not found !", datastoreName));
            }

            return dsSummaries[0].GetDatastore();
        }
    }
}