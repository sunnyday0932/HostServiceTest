using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace HostServiceTest.Infrastructure
{
    public class HostService : BackgroundService
    {
        private readonly string _filePath;

        public HostService(string filePath)
        {
            this._filePath = filePath;
        }

        /// <summary>
        /// This method is called when the <see cref="T:Microsoft.Extensions.Hosting.IHostedService" /> starts. The implementation should return a task that represents
        /// the lifetime of the long running operation(s) being performed.
        /// </summary>
        /// <param name="stoppingToken">Triggered when <see cref="M:Microsoft.Extensions.Hosting.IHostedService.StopAsync(System.Threading.CancellationToken)" /> is called.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" /> that represents the long running operations.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (true)
            {
                //寫入log
                await Log(content: "TestHostService");

                //每十秒執行一次
                await Task.Delay(TimeSpan.FromSeconds(value: 10));
            }
        }

        /// <summary>
        /// Logs the specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        private async Task Log(string content)
        {
            using StreamWriter sw = new StreamWriter(this._filePath, append: true);
            await sw.WriteLineAsync(value: $"紀錄Log:{content}_{DateTime.Now}");
        }
    }
}
