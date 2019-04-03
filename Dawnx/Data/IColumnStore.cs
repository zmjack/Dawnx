using System;
using System.Collections.Generic;
using System.Text;

namespace Dawnx.Data
{
    public interface IColumnStore
    {
        Guid Id { get; set; }
        string EntityId { get; set; }
        string Key { get; set; }
        string Value { get; set; }
    }
}
