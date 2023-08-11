using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace SaveMailingFees
{
	public class Program
	{
		public Program()
		{
		}
        static string rootFolderPath = @"C:\CombinedLetters";
        static string inputFolderPath = Path.Combine(rootFolderPath, "Input");
        static string archiveFolderPath = Path.Combine(rootFolderPath, "Archive");
        static string outputFolderPath = Path.Combine(rootFolderPath, "Output");
        static void Main(string[] args)
        { 

            using IHost host = CreateHostBuilder(args).Build();

            var fileArchiver = host.Services.GetRequiredService<FileArchiver>();
            var letterProcessor = host.Services.GetRequiredService<LetterProcessor>();
            var reportGenerator = host.Services.GetRequiredService<ReportGenerator>();

            fileArchiver.ArchiveFiles();
            letterProcessor.ProcessLetters();
            reportGenerator.GenerateTextReport(letterProcessor.CombinedLetterCount);

            
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    // Passing all the Paths here
                    services.AddSingleton(new FileArchiver(inputFolderPath, archiveFolderPath));
                    services.AddSingleton(new LetterProcessor(inputFolderPath, outputFolderPath));
                    services.AddSingleton(new ReportGenerator(outputFolderPath));
                });
    }
}



