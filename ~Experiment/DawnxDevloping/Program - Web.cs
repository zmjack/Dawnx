#if USE
using Dawnx.Diagnostics;
using Dawnx.Net.Http;
using SimpleData;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace DawnxDevloping
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcurrentDictionary<ConcurrencyResultId, string> result;
            IEnumerable<string> values;

            void Test(string name, Func<string> func)
            {
                using (PerformanceProbe.Create(name))
                    result = Concurrency.Run(cid => func(), level: 500, threadCount: 100);
                values = result.Select(x => x.Value);
                Console.WriteLine(values.Count(x => x?.StartsWith("<!DOCTYPE html><!--STATUS OK-->") ?? false));
            }

            //Test(nameof(HttpClient500), HttpClient500);
            Test(nameof(DawnxNetWeb500), DawnxNetWeb500);
        }

        static string HttpClient500()
        {
            var httpClient = new HttpClient();
            return httpClient.GetAsync("https://www.baidu.com/").Result.Content.ReadAsStringAsync().Result;
        }

        static string DawnxNetWeb500()
        {
            return Web.Get("https://www.baidu.com");
        }

    }
}
#endif
