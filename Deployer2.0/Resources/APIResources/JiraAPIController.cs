using Deployer2._0.Resources.APIResources;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace Deployer2._0
{
    public class JiraAPIController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public void GetIssuesRelatedTo()
        {
            var client = new RestClient("http://10.0.88.155:8080/rest/api/2/issue");
            var request = new RestRequest(Method.POST);
            request.AddHeader("postman-token", "fe6a29e6-59da-73f7-56c8-f2f0eba472f1");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", "Basic YWRtaW46TnUxNDA4NTkyNDYh");
            request.AddParameter("application/json", "{\n\t\"fields\":{\n\t\t\"project\":\n\t\t{\n\t\t\t\"key\":\"DEM\"\n\t\t},\n\t\t\"summary\": \"Another Test\",\n\t\t\"description\": \"Another Test Description\",\n\t\t\"issuetype\":{\n\t\t\t\"name\":\"Story\"\n\t\t}\n\t}\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
        }
        private RestRequest RequestStructure(int AmountOfIssuesToRetrieve)
        {
            
            var request = new RestRequest(Method.POST);
            request.AddHeader("postman-token", "67bcde3a-5909-139d-cbe7-89f65da8ae14");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", "Basic YWRtaW46TnUxNDA4NTkyNDYh");
            request.AddParameter("application/json", "{\n\t\"jql\":\"project = DemoProject\",\n\t\"startAt\":0,\n\t\"maxResults\":" + AmountOfIssuesToRetrieve+",\n\t\"fields\":[\"id\",\"key\",\"status\",\"priority\",\"customfield_10007\"]\n\t\n\t\n}\n", ParameterType.RequestBody);
            return request;
        }
        private RestClient getClient()
        {
            var client = new RestClient("http://10.0.88.155:8080/rest/api/2/search");
            return client;
        }
        public List<Issue> getIssues(int AmountOfIssuesToRetrieve)
        {
            
            IRestResponse<RootObject> response = getClient().Execute<RootObject>(RequestStructure(AmountOfIssuesToRetrieve));
            List<Issue> issues = response.Data.issues;
            return issues;
        }
        public RootObject getAllIssues()
        {
            int amountOfIssues = 1;
            IRestResponse<RootObject> response1 = getClient().Execute<RootObject>(RequestStructure(amountOfIssues));
            IRestResponse<RootObject> response2 = null;
            if (response1.Data.total >= 0)
            {
                response2 = getClient().Execute<RootObject>(RequestStructure(response1.Data.total));
            }
            var stuff = response2.Data;
            return stuff;
            
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
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