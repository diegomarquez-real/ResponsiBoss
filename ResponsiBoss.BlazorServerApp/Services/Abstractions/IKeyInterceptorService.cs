namespace ResponsiBoss.BlazorServerApp.Services.Abstractions
{
    public interface IKeyInterceptorService
    {
        Task CreateFormKeyInterceptor(string formClass, string targetClass, Func<Task> formFunc);
    }
}