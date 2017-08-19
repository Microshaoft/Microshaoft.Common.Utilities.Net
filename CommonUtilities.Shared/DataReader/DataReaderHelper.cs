
namespace Microshaoft
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System;
    using System.Reflection;

    public static class DataReaderExtensionsMethodsManager
    {
        public static IEnumerable<T> ExecuteRead<T>
                (
                    this IDataReader target
                    , Func<int, IDataReader, T> onReadProcessFunc
                )
        {
            try
            {
                int i = 0;
                while (target.Read())
                {
                    if (onReadProcessFunc != null)
                    {
                        yield return
                            onReadProcessFunc(++i, target);
                    }
                }
            }
            finally
            {
                //可能有错 由于 yield 延迟
                target.Close();
                target.Dispose();
            }


        }
    }


    public static class DataReaderHelper
    {

        public static IEnumerable<TEntry> AsEnumerable<TEntry>
                        (
                            this SqlDataReader target
                            , bool needDefinitionAttributeProcess = false
                        )
                            where TEntry : new()
        {
            return
                GetEnumerable<TEntry>
                    (
                        target
                        , needDefinitionAttributeProcess
                    );
        } 


        public static IEnumerable<TEntry> GetEnumerable<TEntry>
                (
                    IDataReader dataReader
                    , Func<IDataReader, TEntry> onReadProcessFunc
                    , bool skipNull = true
                )
                    where TEntry : new()
        {
            while (dataReader.Read())
            {
                var x = onReadProcessFunc(dataReader);
                if (!skipNull)
                {
                    yield
                        return
                                x;
                }
                else
                {
                    if (x != null)
                    {
                        yield
                           return
                                   x;
                    }
                }
            }


        }




        public static IEnumerable<TEntry> GetEnumerable<TEntry>
                (
                    IDataReader dataReader
                    , bool needDefinitionAttributeProcess = false
                )
                    where TEntry : new()
        {
            var type = typeof(TEntry);
            MemberAdditionalDefinitionAttribute attribute = null;
            var members = TypeHelper
                                .GetModelMembers(type)
                                .Select
                                    (
                                        (x) =>
                                        {
                                            attribute =
                                                        x
                                                            .GetCustomAttributes
                                                                (typeof(MemberAdditionalDefinitionAttribute), true)
                                                            .FirstOrDefault() as MemberAdditionalDefinitionAttribute;
                                            return x;
                                        }
                                    );
            while (dataReader.Read())
            {
                TEntry entry = new TEntry();
                foreach (var x in members)
                {
                    var dataColumnName = x.Name;
                    if (needDefinitionAttributeProcess)
                    {
                        if (attribute != null)
                        {
                            if (!attribute.DataTableColumnName.IsNullOrEmptyOrWhiteSpace())
                            {
                                dataColumnName = attribute.DataTableColumnName;
                            }
                        }
                    }
                    var setter = DynamicExpressionTreeHelper
                                            .CreateMemberSetter<TEntry, object>
                                                (
                                                    x.Name
                                                );
                    setter
                            (
                                entry
                                , dataReader[dataColumnName]
                            );
                }
                yield
                    return
                        entry;
            }
        } 

    }
}


