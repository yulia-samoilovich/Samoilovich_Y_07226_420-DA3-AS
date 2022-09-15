using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samoilovich_Y_07226_420_DA3_AS.Utils
{
    internal class DbUtils
    {
        private static readonly string DEFAULT_DB_FILE_NAME = "lab.mdf";

        public static readonly string EXECUTION_DIRECTORY =
            Path.GetFullPath(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));

        public static readonly string DEFAULT_DB_FILE_PATH = Path.GetFullPath(
            EXECUTION_DIRECTORY + Path.DirectorySeparatorChar +
            ".." + Path.DirectorySeparatorChar +
            ".." + Path.DirectorySeparatorChar + DEFAULT_DB_FILE_NAME);
        public static SqlConnection GetDefaultConnection()
        {
            string connectionStringLiteral = $"Server=.\\SQL2019EXPRESS; " +
                $"Integrated_security=true; " +
                $"AttachDbFilename={DEFAULT_DB_FILE_PATH}; " +
                $"User Instance=true;";

            SqlConnection connection = new SqlConnection(connectionStringLiteral);
            return connection;

        }
    }
}
