using NUnit.Framework;
using System;
using System.Linq;
using Vaetech.Collections.Generic.Extensions;

namespace Vaetech.Collections.Generic.NUnit
{
    public class Tests
    {        
        [Test]
        public void ConvertListToArrayList_KeyValue()
        {
            Parameters parameters = new Parameters();
            parameters.Add("key", "key...");
            parameters.Add("x-amz-credential", "x-amz-credential...");
            parameters.Add("x-amz-algorithm", "x-amz-algorithm...");
            parameters.Add("x-amz-date", "x-amz-date...");
            parameters.Add("x-amz-signature", "x-amz-signature...");
            parameters.Add("policy", "policy...");
            parameters.Add("acl", "acl...");
            parameters.Add("Content-Type", "Content-Type...");
            parameters.Add("success_action_status", "success_action_status...");           

            // Convert List<Parameter> to ArrayList<TKey,TValue>.
            var result = parameters.ToArrayList(k => k.Name, v => v.Value.ToString());
            // Convert List<Parameter> to ArrayList<TKey,TValue1,TValue2>.
            var result2 = parameters.ToArrayList(k => k.Name, v1 => v1.Value, v2 => v2.DbType);

            string key = result["key"];
            string x_amz_credential = result["x-amz-credential"];
            string x_amz_algorithm = result["x-amz-algorithm"];
            string x_amz_date = result["x-amz-date"];
            string x_amz_signature = result["x-amz-signature"];
            string policy = result["policy"];
            string acl = result[key: "acl"];
            string content_Type = result[key: "Content-Type"];
            string success_action_status = result[key: "success_action_status"];

            Assert.AreEqual(key, parameters.Where(c => c.Name == "key").FirstOrDefault().Value);
            Assert.AreEqual(x_amz_credential, parameters.Where(c => c.Name == "x-amz-credential").FirstOrDefault().Value);
            Assert.AreEqual(x_amz_algorithm, parameters.Where(c => c.Name == "x-amz-algorithm").FirstOrDefault().Value);
            Assert.AreEqual(x_amz_date, parameters.Where(c => c.Name == "x-amz-date").FirstOrDefault().Value);
            Assert.AreEqual(x_amz_signature, parameters.Where(c => c.Name == "x-amz-signature").FirstOrDefault().Value);
            Assert.AreEqual(policy, parameters.Where(c => c.Name == "policy").FirstOrDefault().Value);
            Assert.AreEqual(acl, parameters.Where(c => c.Name == "acl").FirstOrDefault().Value);
            Assert.AreEqual(content_Type, parameters.Where(c => c.Name == "Content-Type").FirstOrDefault().Value);
            Assert.AreEqual(success_action_status, parameters.Where(c => c.Name == "success_action_status").FirstOrDefault().Value);
        }
        [Test]
        public void GetParametersByName()
        {
            Parameters parameters = new Parameters();
            parameters.Add("key", "key...");
            parameters.Add("x-amz-credential", "x-amz-credential...");
            parameters.Add("x-amz-algorithm", "x-amz-algorithm...");
            parameters.Add("x-amz-date", "x-amz-date...");
            parameters.Add("x-amz-signature", "x-amz-signature...");
            parameters.Add("policy", new byte[0]);
            parameters.Add("acl", 1234);            
            parameters.Add("Content-Type", "Content-Type...");
            parameters.Add("success_action_status", "success_action_status...");

            var @params = parameters.ToParameters(n=>n.Name, v=>v.Value);

            Assert.AreEqual(parameters[name: "key"].Value, parameters.Where(c => c.Name == "key").FirstOrDefault().Value);
            Assert.AreEqual(parameters[name: "x-amz-credential"].Value, parameters.Where(c => c.Name == "x-amz-credential").FirstOrDefault().Value);
            Assert.AreEqual(parameters[name: "x-amz-algorithm"].Value, parameters.Where(c => c.Name == "x-amz-algorithm").FirstOrDefault().Value);
            Assert.AreEqual(parameters[name: "x-amz-date"].Value, parameters.Where(c => c.Name == "x-amz-date").FirstOrDefault().Value);
            Assert.AreEqual(parameters[name: "x-amz-signature"].Value, parameters.Where(c => c.Name == "x-amz-signature").FirstOrDefault().Value);
            Assert.AreEqual(parameters[name: "policy"].Value, parameters.Where(c => c.Name == "policy").FirstOrDefault().Value);
            Assert.AreEqual(parameters[name: "acl"].Value, parameters.Where(c => c.Name == "acl").FirstOrDefault().Value);
            Assert.AreEqual(parameters[name: "Content-Type"].Value, parameters.Where(c => c.Name == "Content-Type").FirstOrDefault().Value);
            Assert.AreEqual(parameters[name: "success_action_status"].Value, parameters.Where(c => c.Name == "success_action_status").FirstOrDefault().Value);
        }

        [Test]
        public void ToArrayListWithConvertTypes()
        {
            Parameters parameters = new Parameters();
            parameters.Add("key", "key...");
            parameters.Add("x-amz-credential", "x-amz-credential...");
            parameters.Add("x-amz-algorithm", "x-amz-algorithm...");
            parameters.Add("x-amz-date", "x-amz-date...");
            parameters.Add("x-amz-signature", "x-amz-signature...");
            parameters.Add("policy", "policy...");
            parameters.Add("acl", "acl...");
            parameters.Add("Content-Type", "Content-Type...");
            parameters.Add("success_action_status", "success_action_status...");

            #region samples
            var r1 = parameters.ToArrayList(k => k.Name, v => v.Value); //ok
            var r2 = parameters.ToArrayList(k => k.Name, v => v.Value.ToString());//ok
            var r3 = parameters.ToArrayList(k => k.Name, v => (string)v.Value); //ok
            var r4 = parameters.ToArrayList(k => k.Name, v => Convert.ToString(v.Value));//ok
            var r5 = parameters.ToArrayList(k => k.Name, v => v.Value.ToString().ToLower());//ok
            var r6 = parameters.ToArrayList(k => k.Name, v => v.Value.ToString().Trim().ToLower());//ok
            var r7 = parameters.ToArrayList(k => k.Name, v => v.Value.ToString().TrimStart().TrimEnd().ToUpper());//ok
            var r8 = parameters.ToArrayList(k => k.Name, v => ((string)v.Value ?? "").Trim().ToUpper());//ok

            //var r9 = parameters.ToArrayList(k => k.Name, v => (Convert.ToString(v.Value)??"").ToUpper());
            //var r10 = parameters.ToArrayList(k => k.Name, v => (((string)v.Value)??"").ToArray());
            //var r11 = parameters.ToArrayList(k => k.Name, v1 => v1.Value, v2 => Parameters.TypeMap[v2.Value.GetType()]);

            #endregion
        }
        }
    }