using Dawnx.Reflection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Dynamic;
using System.Text;

namespace Dawnx
{
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
                foreach (var i in Range.Create(reader.FieldCount))
                    dict[reader.GetName(i)] = reader.GetValue(i);

                yield return dict;
            }
            reader.Close();
        }
        
        public TDbCommand SqlCommand(FormattableString formattableSql)
        {
            var values = formattableSql.GetArguments();
            var sql = formattableSql.Format;

            var range = Range.Create(formattableSql.ArgumentCount);
            foreach (var i in range)
                sql = sql.Replace($"{{{i}}}", $"@p{i}");

            var command = new TDbCommand().Self(_ =>
            {
                _.CommandText = sql;
                _.Connection = Connection;
            });

            foreach (var i in range)
            {
                command.Parameters.Add(new TDbParameter().Self(_ =>
                {
                    _.ParameterName = $"@p{i}";
                    _.Value = values[i];
                    _.DbType = values[i].For(value =>
                    {
                        switch (value.GetType().FullName)
                        {
                            case BasicType.@bool: return DbType.Boolean;
                            case BasicType.@byte: return DbType.Byte;
                            case BasicType.@sbyte: return DbType.SByte;
                            case BasicType.@char: return DbType.Byte;
                            case BasicType.@short: return DbType.Int16;
                            case BasicType.@ushort: return DbType.UInt16;
                            case BasicType.@int: return DbType.Int32;
                            case BasicType.@uint: return DbType.UInt32;
                            case BasicType.@long: return DbType.Int64;
                            case BasicType.@ulong: return DbType.UInt64;
                            case BasicType.@float: return DbType.Single;
                            case BasicType.@double: return DbType.Double;
                            case BasicType.@string: return DbType.String;
                            case BasicType.@decimal: return DbType.Decimal;
                            case BasicType.DateTime: return DbType.DateTime;
                            default: return DbType.Object;
                        }
                    });
                }));
            }
            return command;
        }

    }
}
