using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;
using System.Threading.Tasks;
using System.Threading;

namespace Data.Interceptors;

internal class LoggingInterceptor : DbCommandInterceptor
{
    public LoggingInterceptor()
    {
        
    }
     
    public override InterceptionResult<DbCommand> CommandCreating(CommandCorrelatedEventData eventData, InterceptionResult<DbCommand> result)
    {
        return base.CommandCreating(eventData, result);
    }

    public override DbCommand CommandCreated(CommandEndEventData eventData, DbCommand result)
    {
        return base.CommandCreated(eventData, result);
    }

    public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
    {
        return base.ReaderExecuting(command, eventData, result);
    }

    public override InterceptionResult<object> ScalarExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<object> result)
    {
        return base.ScalarExecuting(command, eventData, result);
    }

    public override InterceptionResult<int> NonQueryExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<int> result)
    {
        return base.NonQueryExecuting(command, eventData, result);
    }

    public override DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
    {
        return base.ReaderExecuted(command, eventData, result);
    }

    public override object ScalarExecuted(DbCommand command, CommandExecutedEventData eventData, object result)
    {
        return base.ScalarExecuted(command, eventData, result);
    }

    public override int NonQueryExecuted(DbCommand command, CommandExecutedEventData eventData, int result)
    {
        return base.NonQueryExecuted(command, eventData, result);
    }

    public override void CommandFailed(DbCommand command, CommandErrorEventData eventData)
    {
        base.CommandFailed(command, eventData);
    }

    public override Task CommandFailedAsync(DbCommand command, CommandErrorEventData eventData, CancellationToken cancellationToken = default)
    {
        return base.CommandFailedAsync(command, eventData, cancellationToken);
    }

    public override void CommandCanceled(DbCommand command, CommandEndEventData eventData)
    {
        base.CommandCanceled(command, eventData);
    }

    public override Task CommandCanceledAsync(DbCommand command, CommandEndEventData eventData, CancellationToken cancellationToken = default)
    {
        return base.CommandCanceledAsync(command, eventData, cancellationToken);
    }



}
