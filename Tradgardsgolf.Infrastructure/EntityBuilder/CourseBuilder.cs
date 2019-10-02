using System;
using System.Collections.Generic;
using System.Text;
using Tradgardsgolf.Infrastructure.Entities;
using Tradgardsgolf.Infrastructure.SharedKernel;

namespace Tradgardsgolf.Infrastructure.EntityBuilder
{
    public class CourseBuilder : BaseEntityBuilder<Course>
    {
        internal CourseBuilder(Course entity) : base(entity)
        {

        }

        public CourseBuilder Name(string name)
        {
            _entity.Name = name;
            return this;
        }

        public CourseBuilder Holes(int holes)
        {
            _entity.Holes = holes;
            return this;
        }

        public CourseBuilder Longitude(double longitude)
        {
            _entity.Longitude = longitude;
            return this;
        }


        public CourseBuilder Latitude(double latitude)
        {
            _entity.Latitude = latitude;
            return this;
        }

        public CourseBuilder CreatedBy(Player player)
        {
            _entity.CreatedById = player.Id;
            return this;
        }
    }
}
