namespace WebCinema.Middleware
{
    public class DbInitializerMiddleware
    {
        private readonly RequestDelegate _next;
        public DbInitializerMiddleware(RequestDelegate next)
        {
            _next = next;

        }
        public Task Invoke(HttpContext context, IServiceProvider serviceProvider, CinemaContext dbContext)
        {

            DbInitializer.Initialize(dbContext);

            return _next.Invoke(context);
        }
    }

    //public static class DbInitializerExtensions
    //{
    //    public static IApplicationBuilder UseDbInitializer(this IApplicationBuilder builder)
    //    {
    //        return builder.UseMiddleware<DbInitializerMiddleware>();
    //    }

    //}
}
