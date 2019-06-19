using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace assignment2
{
    class DbManager
    {
        private readonly string conn_string = @"Data Source=LYSISTRAT\SQLEXPRESS;Initial Catalog=School;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public PrintableList<T> ListOf<T>() where T : new()
        {
            PrintableList<T> list = new PrintableList<T>();
            using (SqlConnection conn = new SqlConnection(conn_string))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"exec Get{typeof(T).Name}", conn))
                {
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            T t = new T();
                            foreach (var prop in typeof(T).GetProperties())
                            {
                                prop.SetValue(t, Convert.ChangeType(rdr[prop.Name], prop.PropertyType));
                            }
                            list.Add(t);
                        }
                    }
                }
            }
            return list;
        }
        public void Add<T>(T t)
        {
            string properties()
            {
                string str = string.Empty;
                foreach (var prop in t.GetType().GetProperties().Skip(1))
                {
                    str += $",@{prop.Name}";
                }
                return str.Remove(0, 1);
            }

            using (SqlConnection conn = new SqlConnection(conn_string))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"insert into {typeof(T).Name} values ({properties()})", conn))
                {
                    foreach (var prop in t.GetType().GetProperties())
                    {
                        cmd.Parameters.Add(new SqlParameter(prop.Name, prop.GetValue(t)));
                    }
                    cmd.ExecuteNonQuery();
                }
            }
            Menu.SuccessMessage = $" {typeof(T).Name} added.";
        }
        public void AddToCourse(XInCourse x)
        {
            bool exists = false;
            using (SqlConnection conn = new SqlConnection(conn_string))
            {
                conn.Open();
                string sqlCommand2 = $"	declare @inserted bit;" +
                    $"if not exists(select * from[{x.Name} in Course] where {x.Name}Id = @{x.Name}Id and courseId = @courseId) " +
                    $"and @courseId in (select CourseId from Course) and @{x.Name}Id in (select {x.Name}Id from {x.Name}) " +
                    $"begin        set @inserted = 1;                end " +
                    $"else begin set @inserted = 0; end select @inserted as isInserted; ";
                using (SqlCommand cmd2 = new SqlCommand(sqlCommand2, conn))
                {
                    cmd2.Parameters.Add(new SqlParameter("courseId", x.CourseId));
                    cmd2.Parameters.Add(new SqlParameter($"{x.Name}Id", x.Id));
                    cmd2.ExecuteNonQuery();
                    using (SqlDataReader rdr = cmd2.ExecuteReader())
                    {

                        while (rdr.Read())
                        {
                            exists = (bool)rdr["isInserted"];
                        }
                    }
                }

                string sqlCommand = $"if @courseId in (select CourseId from Course) and @{x.Name}Id in (select {x.Name}Id from {x.Name})" +
                    $"and not exists(select * from[{x.Name} in Course] where {x.Name}Id = @{x.Name}Id and courseId = @courseId)" +
                    $"begin insert into[{x.Name} in Course] values(@courseId, @{x.Name}Id) end";
                using (SqlCommand cmd = new SqlCommand(sqlCommand, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("courseId", x.CourseId));
                    cmd.Parameters.Add(new SqlParameter($"{x.Name}Id", x.Id));
                    cmd.ExecuteNonQuery();
                }
            }
            if (exists) Menu.SuccessMessage = $" {x.Name} with id {x.Id} added to Course with id {x.CourseId}.";
            else Menu.SuccessMessage = $" {x.Name} not added.\n Check if both {x.Name} and Course exist or if {x.Name} is already in Course.";
        }
    }
    class PrintableList<T> : List<T>
    {
        public override string ToString()
        {
            string rows = string.Empty;
            rows += "\n----------------------------------------------------------------------------------------\n";
            foreach (var prop in typeof(T).GetProperties())
            {
                rows += string.Format(" {0,-18}", prop.Name);
            }
            rows += "\n----------------------------------------------------------------------------------------";

            foreach (T o in this)
            {
                rows += "\n";
                foreach (var prop in o.GetType().GetProperties())
                {
                    if (prop.PropertyType == typeof(DateTime))
                    {
                        rows += string.Format(" {0,-18:d}", prop.GetValue(o));
                    }
                    else if (prop.PropertyType == typeof(Decimal))
                    {
                        rows += string.Format(" {0,-18:c}", prop.GetValue(o));
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
