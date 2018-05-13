using System.Collections.Generic;

namespace DataAccessLayer
{
    /// <summary>
    /// Interface for accessing database data
    /// </summary>
    interface IDAL
    {
        IEnumerable<object> GetData(string code, KeyValuePair<string, object> parameters);
    }
}
