using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tradgardsgolf.ApiClient.Course
{
    public static class CourseExtension
    {
        public static async Task<IResponse<IEnumerable<Course>>> ListAllCourses(this TradgradsgolfApiClient client)
        {
            try
            {
                var response = await client.GetAsync("Course");

                return await client.Response<IEnumerable<Course>>(response);
            }
            catch (Exception ex)
            {
                return client.Response<IEnumerable<Course>>(ex);
            }
        }

        public static async Task<IResponse<Course>> AddCourse(this TradgradsgolfApiClient client, CourseAddRequest request)
        {
            try
            {
                var response = await client.PostAsync("Course", request);

                return await client.Response<Course>(response);
            }
            catch (Exception ex)
            {
                return client.Response<Course>(ex);
            }
        }
    }
}
