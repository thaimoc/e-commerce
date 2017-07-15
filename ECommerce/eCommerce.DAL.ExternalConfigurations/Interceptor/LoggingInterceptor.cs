using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using NLog;

namespace eCommerce.DAL.ExternalConfigurations.Interceptor
{
    public class LoggingInterceptor : IDbCommandInterceptor
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            var log = new LogExecuting<int>(Logger, command, interceptionContext);
            log.LogCallback();
        }

        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            var log = new LogExecuted<int>(Logger, command, interceptionContext);
            log.LogCallback();
        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            var log = new LogExecuting<DbDataReader>(Logger, command, interceptionContext);
            log.LogCallback();
        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            var log = new LogExecuted<DbDataReader>(Logger, command, interceptionContext);
            log.LogCallback();
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            var log = new LogExecuting<object>(Logger, command, interceptionContext);
            log.LogCallback();
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            var log = new LogExecuted<object>(Logger, command, interceptionContext);
            log.LogCallback();
        }
    }

    public abstract class LogExecute<T>
    {
        protected readonly Logger Logger;
        public Action LogCallback { get; }

        protected LogExecute(Logger logger, DbCommand command, DbCommandInterceptionContext<T> interceptionContext)
        {
            Logger = logger;
            LogCallback = () =>
            {
                LogTrace(command, interceptionContext);
                LogAction(command, interceptionContext);
            };
        }

        private void LogTrace(DbCommand command, DbCommandInterceptionContext<T> interceptionContext)
        {
            if (interceptionContext == null) throw new ArgumentNullException(nameof(interceptionContext));
            var initFlags = new[]
            {
                "Edm Metadata",
                "__MigrationHistory",
                "sys.databases",
                "serverproperty"
            };

            if (initFlags.Any(x => command.CommandText.Contains(x)))
                Logger.Info(command.CommandText);
            else
                Logger.Trace(command.CommandText);
        }

        protected abstract void LogAction(DbCommand command, DbCommandInterceptionContext<T> interceptionContext);
    }

    public class LogExecuted<T> : LogExecute<T>
    {
        protected override void LogAction(DbCommand command, DbCommandInterceptionContext<T> interceptionContext)
        {
            if (!interceptionContext.IsAsync)
            {
                Logger.Error($"Command {command.CommandText} failed with exception {interceptionContext.Exception}");
            }
        }

        public LogExecuted(Logger logger, DbCommand command, DbCommandInterceptionContext<T> interceptionContext) : base(logger, command, interceptionContext)
        {
        }
    }

    public class LogExecuting<T> : LogExecute<T>
    {
        protected override void LogAction(DbCommand command, DbCommandInterceptionContext<T> interceptionContext)
        {
            if (!interceptionContext.IsAsync)
            {
                Logger.Warn($"Non-async command used: {command.CommandText}");
            }
        }

        public LogExecuting(Logger logger, DbCommand command, DbCommandInterceptionContext<T> interceptionContext) : base(logger, command, interceptionContext)
        {
        }
    }
}