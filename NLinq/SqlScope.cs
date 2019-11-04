using Dawnx.Ranges;
using Dawnx.Utilities;
using NStandard;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Dawnx.Data
{
    /// <summary>
    /// Easy to use and secure SQL Executor
    /// </summary>
    /// <typeparam name="TDbConnection"></typeparam>
    /// <typeparam name="TDbCommand"></typeparam>
    /// <typeparam name="TDbParameter"></typeparam>
    public abstract class SqlScope<TDbConnection, TDbCommand, TDbParameter>
        : Scope<TDbConnection, SqlScope<TDbConnection, TDbCommand, TDbParameter>>
        where TDbConnection : DbConnection
        where TDbCommand : DbCommand, new()
        where TDbParameter : DbParameter, new()
    {
        private TDbConnection Connection;

        public SqlScope(TDbConnection model) : base(model)
        {
            Connection = model;
            Connection.Open();
        }

        public override void Disposing()
        {
            Connection.Close();
            base.Disposing();
        }

        public int Sql(FormattableString formattableSql)
        {
            var command = SqlCommand(formattableSql);
            return command.ExecuteNonQuery();
        }

        public IEnumerable<Dictionary<string, object>> SqlQuery(FormattableString formattableSql)
        {
            var command = SqlCommand(formattableSql);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var dict = new Dictionary<string, object>();
                foreach (var i in IntegerRange.Create(reader.FieldCount))
                    dict[reader.GetName(i)] = reader.GetValue(i);

                yield return dict;
            }
            reader.Close();
        }

        protected TDbCommand SqlCommand(FormattableString formattableSql)
        {
            var values = formattableSql.GetArguments();
            var sql = formattableSql.Format;

            var range = IntegerRange.Create(formattableSql.ArgumentCount);
            foreach (var i in range)
                sql = sql.Replace($"{{{i}}}", $"@p{i}");

            var command = new TDbCommand().Then(_ =>
            {
                _.CommandText = sql;
                _.Connection = Connection;
            });

            foreach (var i in range)
            {
                command.Parameters.Add(new TDbParameter().Then((Action<TDbParameter>)(_ =>
                {
                    _.ParameterName = $"@p{i}";
                    _.Value = values[i];
                    _.DbType = XObject.For<object, DbType>(values[(int)i], (Func<object, DbType>)(value =>
                       {
                           switch (value.GetType().FullName)
                           {
                               case BasicTypeUtility.@bool: return (DbType)DbType.Boolean;
                               case BasicTypeUtility.@byte: return (DbType)DbType.Byte;
                               case BasicTypeUtility.@sbyte: return (DbType)DbType.SByte;
                               case BasicTypeUtility.@char: return (DbType)DbType.Byte;
                               case BasicTypeUtility.@short: return (DbType)DbType.Int16;
                               case BasicTypeUtility.@ushort: return (DbType)DbType.UInt16;
                               case BasicTypeUtility.@int: return (DbType)DbType.Int32;
                               case BasicTypeUtility.@uint: return (DbType)DbType.UInt32;
                               case BasicTypeUtility.@long: return (DbType)DbType.Int64;
                               case BasicTypeUtility.@ulong: return (DbType)DbType.UInt64;
                               case BasicTypeUtility.@float: return (DbType)DbType.Single;
                               case BasicTypeUtility.@double: return (DbType)DbType.Double;
                               case BasicTypeUtility.@string: return (DbType)DbType.String;
                               case BasicTypeUtility.@decimal: return (DbType)DbType.Decimal;
                               case BasicTypeUtility.DateTime: return (DbType)DbType.DateTime;
                               default: return (DbType)DbType.Object;
                           }
                       }));
                })));
            }
            return command;
        }

    }
}
