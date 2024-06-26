﻿using System;
using Tradgardsgolf.Contracts.Course;

namespace Tradgardsgolf.BlazorWasm.Extensions;

public static class CourseExtensions
{
    public static double GetDistance(this CourseResponse courseResponse, double longitude, double latitude)
    {
        if (courseResponse.Name == "Testbanan")
            return 0;

        var d1 = courseResponse.Latitude * (Math.PI / 180.0);
        var num1 = courseResponse.Longitude * (Math.PI / 180.0);
        var d2 = latitude * (Math.PI / 180.0);
        var num2 = longitude * (Math.PI / 180.0) - num1;
        var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) +
                 Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

        return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
    }
}