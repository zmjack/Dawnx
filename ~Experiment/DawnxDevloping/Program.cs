#if !USE
using Dawnx;
using Dawnx.Sequences;
using Microsoft.EntityFrameworkCore;
using SimpleData;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Dawnx.AspNetCore;
using Dawnx.Diagnostics;
using Dawnx.Test;
using Dawnx.Lock;

namespace DawnxDevloping
{
    public class B_ReportRule
    {
        public enum EReportType
        {
            滚动到货表, 库存满足预测
        }

        public Guid Id { get; set; }

        public Guid BizUnit { get; set; }

        public EReportType ReportType { get; set; }

    }

    class Program
    {
        static void Main(string[] args)
        {
            lock (new InstanceLock<B_ReportRule>(x => x.Id)
                    .InternString(new B_ReportRule { ReportType = B_ReportRule.EReportType.滚动到货表 }))
            {

            }

            using (var sqlite = new NorthwndContext(SimpleSources.NorthwndOptions))
            {
            }
        }


    }
}
#endif
