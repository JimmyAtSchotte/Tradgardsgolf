﻿using System;
using MediatR;

namespace Tradgardsgolf.Contracts.Course;

public class ChangeCourseImage : IRequest<CourseResponse>
{
    public Guid Id { get; set; }
    public string ImageBase64 { get; set; }
    public string Extension { get; set; }
}