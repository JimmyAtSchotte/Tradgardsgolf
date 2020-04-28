using System;
using System.Collections.Generic;
using System.Text;

namespace Tradgardsgolf.Mobile.Core.Interfaces
{
    public interface ISessionStorage
    {
        IEnumerable<string> Courses { get; }

    }
}
