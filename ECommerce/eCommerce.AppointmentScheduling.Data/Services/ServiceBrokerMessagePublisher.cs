using System.Data.SqlClient;
using eCommerce.AppointmentScheduling.Core.Interfaces;
using eCommerce.AppointmentScheduling.Data.Events;
using eCommerce.SharedKernel.Interfaces;
using Newtonsoft.Json;

namespace eCommerce.AppointmentScheduling.Data.Services
{
    public class ServiceBrokerMessagePublisher : IMessagePublisher
    {

        private readonly string ConnectionString = "Data Source=DESKTOP-RI4P4PV;Initial Catalog=eCommerce.DAL.VetOfficeContext;Integrated Security=True;MultipleActiveResultSets=True";
        private readonly string MessageType = "SBMessage";
        private readonly string Contract = "SBContract";
        private readonly string SchedulerService = "SchedulerService";
        private readonly string NotifierService = "NotifierService";

        public void Publish(IApplicationEvent applicationEvent)
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                using (var sqlTransaction = sqlConnection.BeginTransaction())
                {
                    var conversationHandle = ServiceBrokerWrapper.BeginConversation(sqlTransaction, SchedulerService, NotifierService, Contract);

                    string json = JsonConvert.SerializeObject(applicationEvent, Formatting.None);

                    ServiceBrokerWrapper.Send(sqlTransaction, conversationHandle, MessageType, ServiceBrokerWrapper.GetBytes(json));

                    ServiceBrokerWrapper.EndConversation(sqlTransaction, conversationHandle);

                    sqlTransaction.Commit();
                }
                sqlConnection.Close();
            }
        }
    }
}