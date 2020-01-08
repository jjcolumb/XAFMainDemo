using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using DevExpress.Internal;

namespace Demos.Data {
    public static class DemoDbEngineDetectorHelper {
        public static string AlternativeConnectionString = "DataSource=Alternative";
        public static string SQLServerIsNotFoundMessage = "Could not find a SQL database server on your computer.";
        public static string DBServerIsNotAccessibleMessage = "This XAF Demo application failed to access your SQL database server.";
        public static string PatchSQLConnectionString(string connectionString) {
#if !NETSTANDARD
            if(DbEngineDetector.IsSqlExpressInstalled || DbEngineDetector.IsLocalDbInstalled) {
                return DbEngineDetector.PatchConnectionString(connectionString);
            }
            else {
#endif
                return DemoDbEngineDetectorHelper.AlternativeConnectionString;
#if !NETSTANDARD
            }
#endif
        }
        private static string GetSQLServerConnectionString(string connectionString) {
            string result = connectionString;
            List<string> connectionStringParts = new List<string>();
            connectionStringParts.AddRange(connectionString.Split(';'));
            string databaseName = connectionStringParts.FirstOrDefault(x => x.StartsWith("initial catalog", StringComparison.InvariantCultureIgnoreCase));
            if(!string.IsNullOrEmpty(databaseName)) {
                connectionStringParts.Remove(databaseName);
                result = string.Join(";", connectionStringParts);
            }
            return result;
        }
        public static string GetIssueMessage(string connectionString) {
            return connectionString == AlternativeConnectionString ? SQLServerIsNotFoundMessage : DBServerIsNotAccessibleMessage;
        }
        public static bool IsSqlServerAccessible(string connectionString) {
            if(string.IsNullOrEmpty(connectionString)) {
                return false;
            }
            bool result = true;
            string sqlServerConnectionString = GetSQLServerConnectionString(connectionString);
            SqlConnection sqlConnection = new SqlConnection(sqlServerConnectionString);
            try {
                sqlConnection.Open();
            }
            catch(Exception) {
                result = false;
            }
            finally {
                if(sqlConnection != null) {
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                }
            }
            return result;
        }
    }

    public class UseSQLAlternativeInfoSingleton {
        private static UseSQLAlternativeInfoSingleton instance;
        private UseSQLAlternativeInfo useSqlAlternativeInfo;
        private UseSQLAlternativeInfoSingleton() {
            UseAlternative = false;
        }
        public static UseSQLAlternativeInfoSingleton Instance {
            get {
                if(instance == null) {
                    instance = new UseSQLAlternativeInfoSingleton();
                    instance.useSqlAlternativeInfo = new UseSQLAlternativeInfo();
                }
                return instance;
            }
        }
        public bool UseAlternative { get; set; }
        public UseSQLAlternativeInfo Info { get { return useSqlAlternativeInfo; } }
        public void FillFields(string sqlIssue, string alternativeName, string restrictions) {
            if(!this.UseAlternative) {
                this.UseAlternative = true;
                this.Info.SQLIssue = sqlIssue;
                this.Info.Alternative = alternativeName;
                this.Info.Restrictions = restrictions;
            }
            else if(!this.Info.Alternative.Contains(alternativeName)) {
                AddAlternative(alternativeName, restrictions);
            }
        }
        public void AddAlternative(string alternativeName, string restrictions) {
            this.Info.Alternative += " and " + alternativeName;
            this.Info.Restrictions += Environment.NewLine + restrictions;
        }
        public void Clear() {
            UseAlternative = false;
            Info.SQLIssue = null;
            Info.Alternative = null;
            Info.Restrictions = null;
        }
    }
}
