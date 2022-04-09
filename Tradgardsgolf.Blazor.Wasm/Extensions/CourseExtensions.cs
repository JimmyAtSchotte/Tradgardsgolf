﻿using System;
using Tradgardsgolf.Contracts.Course;

namespace Tradgardsgolf.Blazor.Wasm.Extensions
{
    public static class CourseExtensions
    {
        public static double GetDistance(this Course course, double longitude, double latitude)
        {
            if (course.Name == "Testbanan")
                return 0;

            var d1 = course.Latitude * (Math.PI / 180.0);
            var num1 = course.Longitude * (Math.PI / 180.0);
            var d2 = latitude * (Math.PI / 180.0);
            var num2 = longitude * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) +
                     Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

            return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
        }
    }
}