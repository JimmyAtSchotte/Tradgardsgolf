﻿using Microsoft.AspNetCore.Http;
using Tradgardsgolf.Api.ActionFilters;
using Tradgardsgolf.Contracts.Course;
using Tradgardsgolf.Contracts.Types;

namespace Tradgardsgolf.Api.Tests.ActionFilter;

[TestFixture]
public class ImageReferenceMutatorTests
{
    [Test]
    public void MutateImageReferenceObject()
    {
        var response = new ImageReference();
        ImageReferenceMutator.Mutate(new DefaultHttpContext
        {
            Request =
            {
                Host = new HostString("localhost"),
                Scheme = "https"
            }
        }, response);

        Assert.That(response.Url, Is.EqualTo("https://localhost/images/"));
    }

    [Test]
    public void MutateImageReferenceProperty()
    {
        var response = new CourseResponse
        {
            ImageReference = new ImageReference()
        };

        ImageReferenceMutator.Mutate(new DefaultHttpContext
        {
            Request =
            {
                Host = new HostString("localhost"),
                Scheme = "https"
            }
        }, response);

        Assert.That(response.ImageReference.Url, Is.EqualTo("https://localhost/images/"));
    }

    [Test]
    public void MutateArrayObjectWithImageReferenceProperty()
    {
        var response = new[]
        {
            new CourseResponse
            {
                ImageReference = new ImageReference()
            }
        };

        ImageReferenceMutator.Mutate(new DefaultHttpContext
        {
            Request =
            {
                Host = new HostString("localhost"),
                Scheme = "https"
            }
        }, response);

        Assert.That(response[0].ImageReference?.Url, Is.EqualTo("https://localhost/images/"));
    }

    [Test]
    public void MutateEnumerableObjectWithImageReferenceProperty()
    {
        var response = CreateEnumerable().ToList();

        ImageReferenceMutator.Mutate(new DefaultHttpContext
        {
            Request =
            {
                Host = new HostString("localhost"),
                Scheme = "https"
            }
        }, response);

        var result = response.ToArray();

        Assert.That(result[0].ImageReference?.Url, Is.EqualTo("https://localhost/images/"));
    }

    private static IEnumerable<CourseResponse> CreateEnumerable()
    {
        yield return new CourseResponse
        {
            ImageReference = new ImageReference()
        };
    }
}