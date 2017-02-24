using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Casepro
{
   public class DBConnect
    {

        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        public static string conn = "SERVER=" + Helper.serverIP + ";" + "DATABASE="+Helper.db+";" + "UID="+Helper.dbusername+";" + "PASSWORD="+Helper.dbpwd+";";

       // public static string conn = "SERVER="+ Helper.serverIP+";" + "DATABASE=cases;" + "UID=daga;" + "PASSWORD=Case.2016;";
        public static string remoteConn = "SERVER=caseprofessional.org;" + "DATABASE=caseprof_case;"+ "UID=caseprof_case;" + "PASSWORD=Case.2016;Connection Timeout=5";
        //caseprofessional.org
        //50.87.14.145
        //Constructor
        public DBConnect()
        {
          
        }

    
    }
}