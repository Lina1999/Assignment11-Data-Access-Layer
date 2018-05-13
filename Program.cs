using System.Collections.Generic;

namespace DataAccessLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            DAL dal = new DAL();

            var kvp = new KeyValuePair<string, object>("Rate", "descending order");
            //my order by = order by
            dal.GetData("my order by", kvp);

            kvp = new KeyValuePair<string, object>("Rate", 50);
            //my where = where
            dal.GetData("my where", kvp);
            dal.Print();

            kvp = new KeyValuePair<string, object>("RateChangeDate", 0);
            //my group by = group by
            dal.GetData("my group by", kvp);
            dal.Print();

            kvp = new KeyValuePair<string, object>("BusinessEntityID", "BusinessEntityID");
            //my left join = left join
            dal.GetData("my left join", kvp);
            dal.Print();

            kvp = new KeyValuePair<string, object>("BusinessEntityID", "BusinessEntityID");
            //my right join = right join
            dal.GetData("my right join", kvp);
            dal.Print();
        }
    }
}
