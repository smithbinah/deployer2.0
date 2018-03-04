using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Deployer2._0.Models;
using System.Threading.Tasks;
using System.Threading;
using RestSharp.Authenticators;
using System.Security.Cryptography.X509Certificates;
using System.IO;

namespace Deployer2._0.Resources.APIResources
{
    public class VirtualMachineAPIController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }
        public async Task<IRestResponse> CreateVM(VMModel vMModel)
        {
            var client = new RestClient("https://10.0.88.11/rest/vcenter/vm") { Authenticator = new HttpBasicAuthenticator("administrator@vsphere.local", "Nu140859246!") };
            var request = new RestRequest(Method.POST);
            //request.AddHeader("Postman-Token", "b93c4f52-7210-d657-64f2-7d7273e84640");
            //request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            //request.AddHeader("Authorization", "Basic YWRtaW5pc3RyYXRvckB2c3BoZXJlLmxvY2FsOk51MTQwODU5MjQ2IQ==");
            request.AddParameter("undefined", "{\r\n  \"spec\": {\r\n    \"cdroms\": [\r\n      {\r\n        \"backing\": {\r\n         \"device_access_type\": \"PASSTHRU\",\r\n                        \"type\": \"CLIENT_DEVICE\"\r\n          \r\n        },\r\n        \r\n        \"start_connected\": true,\r\n        \"allow_guest_control\": true,\r\n        \"sata\": {\r\n           \"bus\": 0,\r\n                        \"unit\": 0\r\n        },\r\n        \"type\": \"SATA\"\r\n      }\r\n    ],\r\n    \"placement\": {\r\n      \"resource_pool\": \"resgroup-26\",\r\n      \r\n      \"folder\": \"group-v231\",\r\n      \r\n      \"datastore\": \"datastore-27\"\r\n    },\r\n    \"name\": \""+ vMModel.Name+"\",\r\n    \"boot\": {\r\n      \"enter_setup_mode\": true,\r\n      \"retry\": true,\r\n      \r\n     \r\n      \"delay\": 0,\r\n      \"retry_delay\": 0,\r\n      \"type\": \"BIOS\"\r\n    },\r\n    \"disks\": [\r\n      {\r\n       \r\n        \"backing\": {\r\n          \"vmdk_file\": \"[vsanDatastore]\",\r\n          \"type\": \"VMDK_FILE\"\r\n        },\r\n        \r\n        \r\n        \"ide\": {\r\n          \"master\": true,\r\n          \"primary\": true\r\n        },\r\n        \"type\": \"IDE\"\r\n      }\r\n    ],\r\n    \"boot_devices\": [\r\n      {\r\n        \"type\": \"CDROM\"\r\n      }\r\n    ],\r\n    \"hardware_version\": \"VMX_13\",\r\n    \"guest_OS\": \"RHEL_7_64\",\r\n    \"nics\": [\r\n      {\r\n        \"backing\": {\r\n          \"distributed_port\": \"69\",\r\n          \"type\": \"DISTRIBUTED_PORTGROUP\",\r\n          \"network\": \"dvportgroup-32\"\r\n        },\r\n        \"pci_slot_number\": 0,\r\n        \"allow_guest_control\": true,\r\n        \"mac_type\": \"MANUAL\",\r\n        \r\n        \"wake_on_lan_enabled\": true,\r\n        \"start_connected\": true,\r\n        \"mac_address\": \"00:50:56:50:49:9a\",\r\n        \"type\": \"E1000\"\r\n      }\r\n    ],\r\n    \"memory\": {\r\n      \"hot_add_enabled\": true,\r\n      \"size_MiB\":"+ vMModel.Memory+ "\r\n    },\r\n    \"cpu\": {\r\n      \"count\":"+ vMModel.CPUs+ ",\r\n      \"hot_add_enabled\": true,\r\n      \"hot_remove_enabled\": true,\r\n      \"cores_per_socket\": 1\r\n    }\r\n  }\r\n}", ParameterType.RequestBody);
            //IRestResponse response = await client.ExecuteTaskAsync(request);

            //IRestResponse<RootObject> response = client.Execute<RootObject>(request);
            return await client.ExecuteTaskAsync(request); ;
        }
        public async Task<string> Login()
        {
            //var client = new RestClient("https://10.0.88.11/rest/com/vmware/cis/session");
            //var request = new RestRequest(Method.POST);
            //var cancelationtoken = new CancellationTokenSource();
            //request.AddHeader("Postman-Token", "66b790ce-512a-573a-f802-1bfc499bfa45");
            //request.AddHeader("Cache-Control", "no-cache");
            //request.AddHeader("Authorization", "Basic YWRtaW5pc3RyYXRvckB2c3BoZXJlLmxvY2FsOk51MTQwODU5MjQ2IQ==");
            //IRestResponse<VCenterRootObject> response = await client.ExecuteTaskAsync<VCenterRootObject>(request, cancelationtoken.Token);

            //var client = new RestClient("https://10.0.88.11/rest/com/vmware/cis/session");
            //var request = new RestRequest(Method.POST);
            //var cancelationtoken = new CancellationTokenSource();
            //request.AddHeader("Authorization", "Basic YWRtaW5pc3RyYXRvckB2c3BoZXJlLmxvY2FsOk51MTQwODU5MjQ2IQ==");
            //IRestResponse response = await client.ExecuteTaskAsync(request);

            var client = new RestClient("https://10.0.88.11/rest/com/vmware/cis/session");
            client.Authenticator = new SimpleAuthenticator("username", "administrator@vsphere.local", "password", "Nu140859246!");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Basic YWRtaW5pc3RyYXRvckB2c3BoZXJlLmxvY2FsOk51MTQwODU5MjQ2IQ==");
            //request.AddHeader("vmware-use-header-authn", "binah");
            //request.AddHeader("header vmware-api-session-id", "null");

            //IRestResponse response = await client.ExecuteTaskAsync(request);
            IRestResponse<RootObject> response = await
             client.ExecuteTaskAsync<RootObject>(request);

            string data = response.StatusCode.ToString();

            return data;
        }
        public string Login(string str)
        {
            //var client = new RestClient("https://10.0.88.11/rest/com/vmware/cis/session");
            //var request = new RestRequest(Method.POST);
            //var cancelationtoken = new CancellationTokenSource();
            //request.AddHeader("Postman-Token", "66b790ce-512a-573a-f802-1bfc499bfa45");
            //request.AddHeader("Cache-Control", "no-cache");
            //request.AddHeader("Authorization", "Basic YWRtaW5pc3RyYXRvckB2c3BoZXJlLmxvY2FsOk51MTQwODU5MjQ2IQ==");
            //IRestResponse<VCenterRootObject> response = await client.ExecuteTaskAsync<VCenterRootObject>(request, cancelationtoken.Token);

            string link = System.Web.HttpContext.Current.Server.MapPath("~/Resources/e2eaf188.0.crt");

            X509Certificate2 certificate = new X509Certificate2();
            certificate.Import(ReadFile(link));
            var client = new RestClient("https://10.0.88.11/rest/com/vmware/cis/session");
            client.ClientCertificates = new X509CertificateCollection() { certificate };
            var request = new RestRequest(Method.POST);

            request.AddHeader("Authorization", "Basic YWRtaW5pc3RyYXRvckB2c3BoZXJlLmxvY2FsOk51MTQwODU5MjQ2IQ==");
            IRestResponse<RootToken> response = client.Execute<RootToken>(request);
            
            return "Response: " + response.Content +
                " Code: "+ response.StatusCode;
        }
        public static byte[] ReadFile(string fileName)
        {
            FileStream f = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            int size = (int)f.Length;
            byte[] data = new byte[size];
            size = f.Read(data, 0, size);
            f.Close();
            return data;
        }
        public VMRoot ListVMs()
        {
            string link = System.Web.HttpContext.Current.Server.MapPath("~/Resources/e2eaf188.0.crt");
            string scriptDir = $"{link}";
            X509Certificate2 certificate = new X509Certificate2();
            certificate.Import(scriptDir);
            

            var client = new RestClient("https://10.0.88.11/rest/vcenter/vm");
            client.ClientCertificates = new X509CertificateCollection() {certificate};
            var request = new RestRequest(Method.GET);
            request.AddHeader("postman-token", "351c23f4-1c62-4ddc-0c97-f04d7319d081");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "Basic YWRtaW5pc3RyYXRvckB2c3BoZXJlLmxvY2FsOk51MTQwODU5MjQ2IQ==");
            IRestResponse<VMRoot> response = client.Execute<VMRoot>(request);
            return response.Data;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}