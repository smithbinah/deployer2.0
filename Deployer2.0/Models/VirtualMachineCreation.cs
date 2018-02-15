using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using vmware.samples.common;
using vmware.samples.common.authentication;
using vmware.vcenter;
using vmware.vcenter.vm;
using vmware.vcenter.vm.hardware;
using vmware.vcenter.vm.hardware.boot;

namespace Deployer2._0.Models
{
    public class VirtualMachineCreation : SamplesBase
    {
        private vmware.vcenter.VM vmService;
        //private string VmName = "Sample-Exhaustive-VM";
        public string VmName { get; set; }
        private const string SerialPortNetworkServiceLocation =
           "tcp://localhost:16000";
        private static long GB = 1024 * 1024 * 1024;
        private GuestOS vmGuestOS = GuestOS.CENTOS_7;
        private HardwareTypes.Version HARDWARE_VERSION =
                HardwareTypes.Version.VMX_11;
        private string exhaustiveVMId;

        public string DatacenterName { get; set; }
        public string ClusterName { get; set; }
        public string VmFolderName { get; set; }
        public string DatastoreName { get; set; }
        public string StandardPortgroupName { get; set; }
        public string DistributedPortgroupName { get; set; }
        public string IsoDatastorePath { get; set; }
        public Label label { get; set; }

        public VirtualMachineCreation(string VMName, Label label)
        {
            this.label = label;
            this.Server = "10.0.88.11";
            this.UserName = "administrator@vsphere.local";
            this.Password = "Nu140859246!";
            this.SkipServerVerification = true;
            this.ClearData = true;

            this.VmName = VMName;
            this.DatacenterName = "Datacenter";
            this.ClusterName = "VSANCluster";
            this.VmFolderName = "APIVMs";
            this.DatastoreName = "vsanDatastore";
            this.StandardPortgroupName = "APINetwork";
            this.DistributedPortgroupName = "API(DPG)";
            this.IsoDatastorePath = "[vsanDatastore] ISOImages";
            
        }
        public override void Cleanup()
        {
            this.VapiAuthHelper.Logout();
        }

        
        private void CreateVm(VMTypes.PlacementSpec vmPlacementSpec,
            string standardNetworkBacking, string distributedNetworkBacking)
        {
            CpuTypes.UpdateSpec cpuUpdateSpec = new CpuTypes.UpdateSpec();
            cpuUpdateSpec.SetCoresPerSocket(1L);
            cpuUpdateSpec.SetHotAddEnabled(false);
            cpuUpdateSpec.SetHotRemoveEnabled(false);

            MemoryTypes.UpdateSpec memoryUpdateSpec =
                new MemoryTypes.UpdateSpec();
            memoryUpdateSpec.SetSizeMiB(2 * 1024L);
            memoryUpdateSpec.SetHotAddEnabled(false);

            ScsiAddressSpec scsiAddressSpec = new ScsiAddressSpec();
            scsiAddressSpec.SetBus(0L);
            scsiAddressSpec.SetUnit(0L);
            DiskTypes.VmdkCreateSpec vmdkCreateSpec1 =
                new DiskTypes.VmdkCreateSpec();
            vmdkCreateSpec1.SetCapacity(40 * GB);
            vmdkCreateSpec1.SetName("boot");
            DiskTypes.CreateSpec diskCreateSpec1 = new DiskTypes.CreateSpec();
            diskCreateSpec1.SetType(DiskTypes.HostBusAdapterType.SCSI);
            diskCreateSpec1.SetScsi(scsiAddressSpec);
            diskCreateSpec1.SetNewVmdk(vmdkCreateSpec1);

            DiskTypes.VmdkCreateSpec vmdkCreateSpec2 =
                new DiskTypes.VmdkCreateSpec();
            vmdkCreateSpec2.SetCapacity(10 * GB);
            vmdkCreateSpec2.SetName("data1");
            DiskTypes.CreateSpec diskCreateSpec2 = new DiskTypes.CreateSpec();
            diskCreateSpec2.SetNewVmdk(vmdkCreateSpec2);

            DiskTypes.VmdkCreateSpec vmdkCreateSpec3 =
                new DiskTypes.VmdkCreateSpec();
            vmdkCreateSpec3.SetCapacity(10 * GB);
            vmdkCreateSpec3.SetName("data2");
            DiskTypes.CreateSpec diskCreateSpec3 = new DiskTypes.CreateSpec();
            diskCreateSpec3.SetNewVmdk(vmdkCreateSpec3);

            EthernetTypes.BackingSpec nicStandardNetworkBacking =
                new EthernetTypes.BackingSpec();
            nicStandardNetworkBacking.SetNetwork(standardNetworkBacking);
            nicStandardNetworkBacking.SetType(
                EthernetTypes.BackingType.STANDARD_PORTGROUP);
            EthernetTypes.CreateSpec manualEthernetSpec =
                    new EthernetTypes.CreateSpec();
            manualEthernetSpec.SetStartConnected(true);
            manualEthernetSpec.SetMacType(EthernetTypes.MacAddressType.MANUAL);
            manualEthernetSpec.SetMacAddress("11:23:58:13:21:34");
            manualEthernetSpec.SetBacking(nicStandardNetworkBacking);


            EthernetTypes.BackingSpec nicDistributedNetworkBacking =
                new EthernetTypes.BackingSpec();
            nicDistributedNetworkBacking.SetNetwork(distributedNetworkBacking);
            nicDistributedNetworkBacking.SetType(
                EthernetTypes.BackingType.DISTRIBUTED_PORTGROUP);
            EthernetTypes.CreateSpec generatedEthernetSpec =
                new EthernetTypes.CreateSpec();
            generatedEthernetSpec.SetStartConnected(true);
            generatedEthernetSpec.SetMacType(
                EthernetTypes.MacAddressType.GENERATED);
            generatedEthernetSpec.SetBacking(nicDistributedNetworkBacking);

            CdromTypes.BackingSpec cdromBackingSpec =
                new CdromTypes.BackingSpec();
            cdromBackingSpec.SetType(CdromTypes.BackingType.ISO_FILE);
            cdromBackingSpec.SetIsoFile(IsoDatastorePath);
            CdromTypes.CreateSpec cdromCreateSpec =
                    new CdromTypes.CreateSpec();
            cdromCreateSpec.SetBacking(cdromBackingSpec);

            SerialTypes.BackingSpec serialBackingSpec =
                new SerialTypes.BackingSpec();
            serialBackingSpec.SetType(SerialTypes.BackingType.NETWORK_SERVER);
            serialBackingSpec.SetNetworkLocation(
                new Uri(SerialPortNetworkServiceLocation));
            SerialTypes.CreateSpec serialCreateSpec =
                new SerialTypes.CreateSpec();
            serialCreateSpec.SetStartConnected(false);
            serialCreateSpec.SetBacking(serialBackingSpec);

            ParallelTypes.BackingSpec parallelBackingSpec =
                new ParallelTypes.BackingSpec();
            parallelBackingSpec.SetType(ParallelTypes.BackingType.HOST_DEVICE);
            ParallelTypes.CreateSpec parallelCreateSpec =
                    new ParallelTypes.CreateSpec();
            parallelCreateSpec.SetBacking(parallelBackingSpec);
            parallelCreateSpec.SetStartConnected(false);

            // Floppy CreateSpec
            FloppyTypes.BackingSpec floppyBackingSpec =
                new FloppyTypes.BackingSpec();
            floppyBackingSpec.SetType(FloppyTypes.BackingType.CLIENT_DEVICE);
            FloppyTypes.CreateSpec floppyCreateSpec =
                new FloppyTypes.CreateSpec();
            floppyCreateSpec.SetBacking(floppyBackingSpec);

            // Specify the boot order
            DeviceTypes.EntryCreateSpec cdromEntry =
                new DeviceTypes.EntryCreateSpec();
            cdromEntry.SetType(DeviceTypes.Type.CDROM);

            DeviceTypes.EntryCreateSpec diskEntry =
                new DeviceTypes.EntryCreateSpec();
            diskEntry.SetType(DeviceTypes.Type.DISK);

            DeviceTypes.EntryCreateSpec ethernetEntry =
                new DeviceTypes.EntryCreateSpec();
            ethernetEntry.SetType(DeviceTypes.Type.ETHERNET);

            List<DeviceTypes.EntryCreateSpec> bootDevices =
                new List<DeviceTypes.EntryCreateSpec> { cdromEntry, diskEntry,
                ethernetEntry };

            VMTypes.CreateSpec vmCreateSpec = new VMTypes.CreateSpec();
            vmCreateSpec.SetBootDevices(bootDevices);
            vmCreateSpec.SetCdroms(
                new List<CdromTypes.CreateSpec> { cdromCreateSpec });
            vmCreateSpec.SetCpu(cpuUpdateSpec);
            vmCreateSpec.SetDisks(new List<DiskTypes.CreateSpec> {
                diskCreateSpec1, diskCreateSpec2, diskCreateSpec3 });
            vmCreateSpec.SetFloppies(
                new List<FloppyTypes.CreateSpec> { floppyCreateSpec });
            vmCreateSpec.SetHardwareVersion(HARDWARE_VERSION);
            vmCreateSpec.SetMemory(memoryUpdateSpec);
            vmCreateSpec.SetGuestOS(vmGuestOS);
            vmCreateSpec.SetName(VmName);
            vmCreateSpec.SetNics(
                new List<EthernetTypes.CreateSpec> { manualEthernetSpec,
                    generatedEthernetSpec });
            vmCreateSpec.SetParallelPorts(
                new List<ParallelTypes.CreateSpec> { parallelCreateSpec });
            vmCreateSpec.SetPlacement(vmPlacementSpec);
            vmCreateSpec.SetSerialPorts(
                new List<SerialTypes.CreateSpec> { serialCreateSpec });


            this.vmService = VapiAuthHelper.StubFactory.CreateStub<vmware.vcenter.VM>(
                SessionStubConfiguration);
            //this.exhaustiveVMId = await vmService.CreateAsync(vmCreateSpec);

            //VMTypes.Info vmInfo = await vmService.GetAsync(this.exhaustiveVMId);

        }
        public  void Login()
        {
            
        }

        public override async void Run()
        {
            SetupSslTrustForServer();
            this.VapiAuthHelper = new VapiAuthenticationHelper();

            this.SessionStubConfiguration = await
                 VapiAuthHelper.LoginByUsernameAndPasswordAsync(
                    this.Server, this.UserName, this.Password);

            
            label.Text = SessionStubConfiguration.ToString();

            VMTypes.PlacementSpec vmPlacementSpec =
                PlacementHelper.GetPlacementSpecForCluster(
                    VapiAuthHelper.StubFactory,
                    SessionStubConfiguration,
                    DatacenterName,
                    ClusterName,
                    VmFolderName,
                    DatastoreName);


            //string standardNetworkBacking =
            //    NetworkHelper.GetStandardNetworkBacking(
            //        VapiAuthHelper.StubFactory, SessionStubConfiguration,
            //        DatacenterName, StandardPortgroupName);

            //string distributedNetworkBacking =
            //    NetworkHelper.GetDistributedNetworkBacking(
            //        VapiAuthHelper.StubFactory, SessionStubConfiguration,
            //        DatacenterName, DistributedPortgroupName);

            //CreateVm(vmPlacementSpec, standardNetworkBacking,
            //    distributedNetworkBacking);
            //Cleanup();
        }

        public override Task<string> Run(string str)
        {
            throw new NotImplementedException();
        }
    }
}