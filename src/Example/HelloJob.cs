using Microsoft.Extensions.Options;
using Quartz;
using System;
using System.Threading.Tasks;

namespace Example
{
    class HelloJob : IJob
    {
        private readonly string _message;

        public HelloJob(IOptions<HelloConfig> options)
        {
            _message = options.Value.Message;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine(_message);
        }
    }
}