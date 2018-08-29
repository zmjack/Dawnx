using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Xunit;

namespace Dawnx.Test._Scopes
{
    public class SqlScopeTests
    {
        [Fact]
        public void Test1()
        {
            //TODO: Need to be normalized
            using (var db = new ApplicationDbScope())
            {
                var s = db.SqlQuery($"select * from lottery_draw where Id={10}").ToArray();
            }
        }

        public class ApplicationDbScope : SqlScope<MySqlConnection, MySqlCommand, MySqlParameter>
        {
            public ApplicationDbScope(MySqlConnection model) : base(model)
            {
            }

            public ApplicationDbScope()
                : this(new MySqlConnection("server=127.0.0.1;user=root;pwd=.;database=test"))
            {
            }
        }

    }
}
