using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vmware.vapi.bindings;
using vmware.vcenter;

namespace Deployer2._0.Models
{
    public class DatacenterHelper
    {
        public static String GetDatacenter(
            StubFactory stubFactory, StubConfiguration sessionStubConfig,
            string datacenterName)
        {
            Datacenter datacenterService =
                stubFactory.CreateStub<Datacenter>(sessionStubConfig);
            HashSet<String> datacenterNames = new HashSet<String>
            {
                datacenterName
            };
            DatacenterTypes.FilterSpec dcFilterSpec =
                new DatacenterTypes.FilterSpec();
            dcFilterSpec.SetNames(datacenterNames);
            List<DatacenterTypes.Summary> dcSummaries =
                datacenterService.List(dcFilterSpec);

            if (dcSummaries.Count > 1)
            {
                throw new Exception(String.Format("More than one datacenter" +
                    " with the specified name {0} exist", datacenterName));
            }

            if (dcSummaries.Count <= 0)
            {
                throw new Exception(String.Format("Datacenter with name {0}" +
                    " not found !", datacenterName));
            }

            return dcSummaries[0].GetDatacenter();
        }
    }
}