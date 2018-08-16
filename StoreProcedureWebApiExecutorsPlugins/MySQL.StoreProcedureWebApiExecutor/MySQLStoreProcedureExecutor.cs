﻿namespace Microshaoft.StoreProcedureExecutors
{
    using Microshaoft;
    using MySql.Data.MySqlClient;
    using Newtonsoft.Json.Linq;
    using System.Composition;
    [Export(typeof(IStoreProcedureExecutable))]
    public class MySQLStoreProcedureExecutor
                        : IStoreProcedureExecutable
                            , IStoreProcedureParametersSetCacheAutoRefreshable
    {
        public string DataBaseType => "mysql";////this.GetType().Name;

        public int CachedExecutingParametersExpiredInSeconds
        {
            get;
            set;
        }
        public bool NeedAutoRefreshExecutedTimeForSlideExpire
        {
            get;
            set;
        }
        public bool Execute
                    (
                        string connectionString
                        , string storeProcedureName
                        , JToken parameters
                        , out JToken result
                        , int commandTimeoutInSeconds       = 90   
                    )
        {
            if
                (
                    CachedExecutingParametersExpiredInSeconds > 0
                    &&
                    Microshaoft.MySqlHelper
                        .CachedExecutingParametersExpiredInSeconds
                    !=
                    CachedExecutingParametersExpiredInSeconds
                )
            {
                Microshaoft.MySqlHelper
                        .CachedExecutingParametersExpiredInSeconds
                            = CachedExecutingParametersExpiredInSeconds;
            }
            result = null;
            MySqlConnection connection = new MySqlConnection(connectionString);
            //connection.ConnectionTimeout = connectionTimeoutInSeconds;
            result = Microshaoft.MySqlHelper
                            .StoreProcedureExecute
                                    (
                                        connection
                                        , storeProcedureName
                                        , parameters
                                        , commandTimeoutInSeconds
                                    );
            if (NeedAutoRefreshExecutedTimeForSlideExpire)
            {
                Microshaoft.MySqlHelper
                    .RefreshCachedStoreProcedureExecuted
                        (
                            connection
                            , storeProcedureName
                        );
            }
            return true;
        }
    }
}
