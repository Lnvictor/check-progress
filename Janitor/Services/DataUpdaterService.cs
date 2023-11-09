
using System;
using DatabaseUtils;
using DatabaseUtils.Models;
using DatabaseUtils.Utils;
using Npgsql;

namespace Janitor.Services
{
    public class DataUpdaterService : IHostedService
    {

        private const string  FilesBasePath = "C:\\Check_Progress";
        private IConfiguration _configuration;

        public DataUpdaterService(IConfiguration config) {
            _configuration = config;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("########### Iniciando Processo DbUpdater ################");
            DateTime now;
            while (true)
            {
                Console.WriteLine("Esperando dar a hora de registrar no banco o progresso....");
                now = DateTime.Now;
                if (now.Hour == 23 && now.Minute == 59 && now.Second == 59)
                {
                    UpdateData(now);
                }
                Thread.Sleep(TimeSpan.FromSeconds(30));
            }
        }

        private void UpdateData(DateTime now)
        {
            var answers = FileReader.ReadDayFromFile(now);
            string date = now.ToString("yyyy-MM-dd");
            SaveDayOnDb(date, answers);
        }

        private void SaveDayOnDb(string date, ConstancyLog answers)
        {
            DbConnection dbConnection = DbConnection.GetInstance(_configuration.GetConnectionString("devdb"));
            var conn = dbConnection.GetConnection();

            string insertStatement = $"INSERT INTO public.constancy_logs (exercise, study, work_focused, \"date\")" +
                                     $"VALUES ({answers.Exercise}, {answers.Study}, {answers.WorkFocused}, '{date}');";

            using (NpgsqlCommand cmd = new NpgsqlCommand(insertStatement, conn))
            {
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0) {
                    throw new Exception("Deu coisa errada ai mermao");
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
