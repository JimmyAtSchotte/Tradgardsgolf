using System;
using System.Collections;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Tradgardsgolf.Contracts;
using Tradgardsgolf.Contracts.Types;

namespace Tradgardsgolf.Api.ActionFilters;

public class ImageReferenceMutator
{
    private readonly HttpContext _context;
    private readonly object _response;

    private ImageReferenceMutator(HttpContext context, object response)
    {
        _context = context;
        _response = response;
    }

    private void Mutate()
    {
        if(_response is null)
            return;
        
        MutateObject(_response);
    }
    
    private void MutateObject(object currentObject)
    {
        switch (currentObject)
        {
            case Array array:
            {
                for (var i = 0; i < array.Length; i++)
                {
                    if (array.GetValue(i) is not null)
                        MutateObject(array.GetValue(i));
                }
                return;
            }
            case IEnumerable enumerable:
            {
                foreach (var obj in enumerable)
                    MutateObject(obj);
                
                return;
            }
            case ImageReference imageReference:
            {
                MutateImageReference(imageReference);
                return;
            }
            default:
            {
                var imageReferences = currentObject.GetType().GetProperties()
                    .Where(x => x.PropertyType == typeof(ImageReference));

                foreach (var imageReferenceProperty in imageReferences)
                {
                    if (imageReferenceProperty.GetValue(currentObject) is not ImageReference original) 
                        continue;
                    
                    MutateImageReference(original);
                    imageReferenceProperty.SetValue(currentObject, original);
                }
                return;
            }
        }
    }

    private void MutateImageReference(ImageReference original)
    {
        original.Url = $"{_context.Request.Scheme}://{_context.Request.Host}/images/";
    }

    public static void Mutate(HttpContext context, object response)
    {
        var mutator = new ImageReferenceMutator(context, response);
        mutator.Mutate();
    }
}