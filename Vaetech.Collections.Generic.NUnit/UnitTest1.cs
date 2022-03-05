using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Vaetech.Collections.Generic.Extensions;

namespace Vaetech.Collections.Generic.NUnit
{
    public class Tests
    {        
        [Test]
        public void ConvertListToArrayList_KeyValue()
        {
            Request request = new Request();
            request.AddParameter("key", "key...");
            request.AddParameter("x-amz-credential", "x-amz-credential...");
            request.AddParameter("x-amz-algorithm", "x-amz-algorithm...");
            request.AddParameter("x-amz-date", "x-amz-date...");
            request.AddParameter("x-amz-signature", "x-amz-signature...");
            request.AddParameter("policy", "policy...");
            request.AddParameter("acl", "acl...");
            request.AddParameter("Content-Type", "Content-Type...");
            request.AddParameter("success_action_status", "success_action_status...");

            var result = request.Parameters.ToArrayList(k => k.Name, v => v.Value);

            var key = result["key"].Value1;
            var x_amz_credential = result["x-amz-credential"].Value1;
            var x_amz_algorithm = result["x-amz-algorithm"].Value1;
            var x_amz_date = result["x-amz-date"].Value1;
            var x_amz_signature = result["x-amz-signature"].Value1;
            var policy = result["policy"].Value1;
            var acl = result["acl"].Value1;
            var content_Type = result["Content-Type"].Value1;
            var success_action_status = result["success_action_status"].Value1;

            Assert.AreEqual(key, request.Parameters.Where(c => c.Name == "key").FirstOrDefault().Value);
            Assert.AreEqual(x_amz_credential, request.Parameters.Where(c => c.Name == "x-amz-credential").FirstOrDefault().Value);
            Assert.AreEqual(x_amz_algorithm, request.Parameters.Where(c => c.Name == "x-amz-algorithm").FirstOrDefault().Value);
            Assert.AreEqual(x_amz_date, request.Parameters.Where(c => c.Name == "x-amz-date").FirstOrDefault().Value);
            Assert.AreEqual(x_amz_signature, request.Parameters.Where(c => c.Name == "x-amz-signature").FirstOrDefault().Value);
            Assert.AreEqual(policy, request.Parameters.Where(c => c.Name == "policy").FirstOrDefault().Value);
            Assert.AreEqual(acl, request.Parameters.Where(c => c.Name == "acl").FirstOrDefault().Value);
            Assert.AreEqual(content_Type, request.Parameters.Where(c => c.Name == "Content-Type").FirstOrDefault().Value);
            Assert.AreEqual(success_action_status, request.Parameters.Where(c => c.Name == "success_action_status").FirstOrDefault().Value);
        }

        public class Request
        {
            public List<Parameter> Parameters = new List<Parameter>();
            public void AddParameter(string key, string value) => Parameters.Add(new Parameter(key,value));
        }
        public class Parameter
        {
            public Parameter() { }
            public Parameter(string key, string value)
            {
                Name = key;
                Value = value;
            }
            public string Name { get; set; }
            public string Value { get; set; }
        }
    }
}