using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace assignment2
{
    class PrintableList<T> : List<T>
    {
        public override string ToString()
        {
            string rows = string.Empty;
            rows += "\n----------------------------------------------------------------------------------------\n";
            foreach (var prop in typeof(T).GetProperties())
            {
                rows+= string.Format(" {0,-18}",prop.Name);
            }
            rows += "\n----------------------------------------------------------------------------------------";

            foreach (T o in this)
            {
                rows += "\n";
                foreach(var prop in o.GetType().GetProperties())
                {
                    if (prop.PropertyType == typeof(DateTime))
                    {
                        rows += string.Format( " {0,-18:d}", prop.GetValue(o));
                    }
                    else if (prop.PropertyType == typeof(Decimal))
                    {
                        rows += string.Format( " {0,-18:c}", prop.GetValue(o));
                    }
                    else
                    {
                        rows += string.Format(" {0,-18}", prop.GetValue(o));
                    }
                }
            }
            if (this.Count == 0)
            {
                rows += "\n No entries";
            }
            return rows;
        }
    }
}
