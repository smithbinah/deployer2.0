using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vmware.vapi.bindings;
using vmware.vcenter;

namespace Deployer2._0.Models
{
    public class FolderHelper
    {
        public static String GetFolder(
            StubFactory stubFactory, StubConfiguration sessionStubConfig,
            string datacenterName, string folderName)
        {
            HashSet<string> datacenters = new HashSet<string>
            {
                DatacenterHelper.GetDatacenter(
                    stubFactory, sessionStubConfig, datacenterName)
            };
            FolderTypes.FilterSpec folderFilterSpec =
                new FolderTypes.FilterSpec();
            folderFilterSpec.SetNames(new HashSet<String> { folderName });
            folderFilterSpec.SetDatacenters(datacenters);

            Folder folderService = stubFactory.CreateStub<Folder>(
                sessionStubConfig);
            List<FolderTypes.Summary> folderSummaries =
                folderService.List(folderFilterSpec);

            if (folderSummaries.Count > 1)
            {
                throw new Exception(String.Format("More than one folder" +
                    " with the specified name {0} exist", folderName));
            }

            if (folderSummaries.Count <= 0)
            {
                throw new Exception(String.Format("Folder with name {0}" +
                                    "not found !", folderName));
            }

            return folderSummaries[0].GetFolder();
        }
    }
}