using System;
using System.Collections.Generic;
using System.Text;
using Tradgardsgolf.Core.SharedKernel.Enums;

namespace Tradgardsgolf.Core.Interfaces.Models
{
    public interface ICreateLoginResult
    {
        CreateLoginStatus Status { get; }
    }
}
