using MudBlazor.Services;
using ResponsiBoss.BlazorServerApp.Services.Abstractions;

namespace ResponsiBoss.BlazorServerApp.Services
{
    public class KeyInterceptorService : IKeyInterceptorService
    {
        private readonly IKeyInterceptor _keyInterceptor;

        public KeyInterceptorService(IKeyInterceptor keyInterceptor)
        {
            _keyInterceptor = keyInterceptor;
        }

        public async Task CreateFormKeyInterceptor(string formClass, string targetClass, Func<Task> formFunc)
        {
            await _keyInterceptor.Connect(formClass, new KeyInterceptorOptions()
            {
                TargetClass = targetClass,
                Keys = new List<KeyOptions>()
            {
                new KeyOptions() { Key = "Enter", PreventDown = "key+none", SubscribeDown = true }
            }
            });

            _keyInterceptor.KeyDown += async (e) =>
            {
                await formFunc.Invoke();
            };
        }
    }
}