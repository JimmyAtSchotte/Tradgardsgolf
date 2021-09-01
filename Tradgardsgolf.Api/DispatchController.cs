using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tradgardsgolf.Contracts;

namespace Tradgardsgolf.Api
{
    [ApiController]
    [Route("api")]
    public class DispatchController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DispatchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("dispatch/{domain}/{typeName}")]
        public async Task<IActionResult> Dispatch(string domain, string typeName, [FromBody] JsonDocument payload,
            CancellationToken cancellationToken)
        {
            var request = DispatchRequestBuilder.Build(domain, typeName, payload);
            var response = await _mediator.Send(request, cancellationToken);
            return new JsonResult(response);
        }
    }
    
    public class DispatchRequestBuilder
    {
        private static readonly string ContractNamespacePrefix = typeof(IContractsNamespaceMarker).Namespace;
        private static readonly string ContractAssemblyName = typeof(IContractsNamespaceMarker).Assembly.FullName;
        
        public static object Build(string domain, string typeName, JsonDocument payload)
        {
            var fullTypeName = $"{ContractNamespacePrefix}.{domain}.{typeName}, {ContractAssemblyName}";
            var requestType = Type.GetType(fullTypeName) ?? 
                              throw new InvalidOperationException($"Type '{domain}.{typeName}' cannot be constructed");

            return JsonSerializer.Deserialize(payload.RootElement.ToString()!, requestType)
                   ?? throw new InvalidOperationException($"Could not deserialize payload for '{domain}.{typeName}'");
        }
    }
}