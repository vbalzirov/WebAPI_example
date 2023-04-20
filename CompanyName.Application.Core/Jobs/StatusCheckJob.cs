using CompanyName.Application.Core.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.Application.Core.Jobs
{
    public class StatusCheckJob
    {
        public StatusCheckJob()
        { }

        public void Start()
        {
            while (true)
            {
                //var orders = Repository.GetOrders();

                //foreach (var order in orders)
                //{
                //    // check ststus
                //    // if date > Now + 30 days
                //    //  status =  overdue;
                //}

                Thread.Sleep(new TimeSpan(0, 10, 0));
            }
        }
    }
}
