using System;
using System.Collections.Generic;
using System.Text;

namespace Tradgardsgolf.Core.Interfaces
{
    public interface IPasswordGenerator
    {
        string Generate(int length);
    }
}
