using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Globalization;

namespace assignment2
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("el-GR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("el-GR");
            Console.OutputEncoding = System.Text.Encoding.Unicode;//Για να εμφανίζει το €

            DbManager db = new DbManager();
            new Menu(db);

        }
    }
}
