using System;
using System.Data;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Xpo;

namespace Demos.Data {
    public static class DemoXPODatabaseHelper {
        public static string InMemoryDatabaseUsageMessage = "This may cause performance issues. All data modifications will be lost when you close the application.";
        public static string AlternativeName = "XPO InMemoryDataStore";
        public static IXpoDataStoreProvider GetInMemoryDataStoreProvider(bool enablePoolingInConnectionString) {
            return XPObjectSpaceProvider.GetDataStoreProvider(InMemoryDataStoreProvider.ConnectionString, null, enablePoolingInConnectionString);
        }
        public static IXpoDataStoreProvider GetDataStoreProvider(string connectionString, IDbConnection connection, bool enablePoolingInConnectionString = true) {
            IXpoDataStoreProvider xpoDataStoreProvider = null;
            if(UseSQLAlternativeInfoSingleton.Instance.UseAlternative || (connectionString == InMemoryDataStoreProvider.ConnectionString)) {
                xpoDataStoreProvider = DemoXPODatabaseHelper.GetInMemoryDataStoreProvider(enablePoolingInConnectionString);
            }
            else {
                if((connection != null) || DemoDbEngineDetectorHelper.IsSqlServerAccessible(connectionString)) {
                    xpoDataStoreProvider = XPObjectSpaceProvider.GetDataStoreProvider(connectionString, connection, enablePoolingInConnectionString);
                }
                else {
                    UseSQLAlternativeInfoSingleton.Instance.FillFields(DemoDbEngineDetectorHelper.DBServerIsNotAccessibleMessage, DemoXPODatabaseHelper.AlternativeName, DemoXPODatabaseHelper.InMemoryDatabaseUsageMessage);
                    xpoDataStoreProvider = DemoXPODatabaseHelper.GetInMemoryDataStoreProvider(enablePoolingInConnectionString);
                }
            }
            return xpoDataStoreProvider;
        }

        public static XPObjectSpaceProvider CreateObjectSpaceProvider(string connectionString, IDbConnection connection, bool threadSafe) {
            string patchedConnectionString = connectionString;
            if(!string.IsNullOrEmpty(connectionString) && (connectionString != InMemoryDataStoreProvider.ConnectionString)) {
                patchedConnectionString = DemoDbEngineDetectorHelper.PatchSQLConnectionString(connectionString);
                if((patchedConnectionString == DemoDbEngineDetectorHelper.AlternativeConnectionString) || !DemoDbEngineDetectorHelper.IsSqlServerAccessible(patchedConnectionString)) {
                    UseSQLAlternativeInfoSingleton.Instance.FillFields(DemoDbEngineDetectorHelper.GetIssueMessage(patchedConnectionString), DemoXPODatabaseHelper.AlternativeName, DemoXPODatabaseHelper.InMemoryDatabaseUsageMessage);
                    patchedConnectionString = InMemoryDataStoreProvider.ConnectionString;
                }
            }
            return new XPObjectSpaceProvider(patchedConnectionString, connection, threadSafe);
        }
    }
}
