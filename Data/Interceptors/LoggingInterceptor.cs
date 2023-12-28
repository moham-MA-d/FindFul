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


    #region Commands Overrides
    public override InterceptionResult<DbCommand> CommandCreating(CommandCorrelatedEventData eventData, InterceptionResult<DbCommand> result)
    {
        return base.CommandCreating(eventData, result);
    }
    public override DbCommand CommandCreated(CommandEndEventData eventData, DbCommand result)
    {
        return base.CommandCreated(eventData, result);
    }
    public override DbCommand CommandInitialized(CommandEndEventData eventData, DbCommand result)
    {
        return base.CommandInitialized(eventData, result);
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
    #endregion



    #region Reader Overrides
    //Called just before EF intends to call ExecuteReader().
    public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
    {
        return base.ReaderExecuting(command, eventData, result);
    }

    //Called just before EF intends to call ExecuteReaderAsync().
    public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result, CancellationToken cancellationToken = default)
    {
        return base.ReaderExecutingAsync(command, eventData, result, cancellationToken);
    }

    //Called immediately after EF calls ExecuteReader().
    public override DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
    {
        return base.ReaderExecuted(command, eventData, result);
    }

    //Called immediately after EF calls ExecuteReaderAsync().
    public override ValueTask<DbDataReader> ReaderExecutedAsync(DbCommand command, CommandExecutedEventData eventData, DbDataReader result, CancellationToken cancellationToken = default)
    {
        return base.ReaderExecutedAsync(command, eventData, result, cancellationToken);
    }
    #endregion





    //Materialization Interceptors run before DataReader
    #region DataReader Overrides

    //Called just before EF intends to call Close().
    public override InterceptionResult DataReaderClosing(DbCommand command, DataReaderClosingEventData eventData, InterceptionResult result)
    {
        return base.DataReaderClosing(command, eventData, result);
    }

    //Called just before EF intends to call CloseAsync() in an async context.
    public override ValueTask<InterceptionResult> DataReaderClosingAsync(DbCommand command, DataReaderClosingEventData eventData, InterceptionResult result)
    {
        return base.DataReaderClosingAsync(command, eventData, result);
    }

    //Called when execution of a DbDataReader is about to be disposed.
    public override InterceptionResult DataReaderDisposing(DbCommand command, DataReaderDisposingEventData eventData, InterceptionResult result)
    {
        return base.DataReaderDisposing(command, eventData, result);
    }
    #endregion






    #region Scalar Overrides
    //Called just before EF intends to call ExecuteScalar().
    public override InterceptionResult<object> ScalarExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<object> result)
    {
        return base.ScalarExecuting(command, eventData, result);
    }

    //Called just before EF intends to call ExecuteScalarAsync().
    public override ValueTask<InterceptionResult<object>> ScalarExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<object> result, CancellationToken cancellationToken = default)
    {
        return base.ScalarExecutingAsync(command, eventData, result, cancellationToken);
    }

    //Called immediately after EF calls ExecuteScalar().
    public override object ScalarExecuted(DbCommand command, CommandExecutedEventData eventData, object result)
    {
        return base.ScalarExecuted(command, eventData, result);
    }

    //Called immediately after EF calls ExecuteScalarAsync().
    public override ValueTask<object> ScalarExecutedAsync(DbCommand command, CommandExecutedEventData eventData, object result, CancellationToken cancellationToken = default)
    {
        return base.ScalarExecutedAsync(command, eventData, result, cancellationToken);
    }
    #endregion



    #region NonQuery Overrides

    //Called just before EF intends to call ExecuteNonQuery().
    public override InterceptionResult<int> NonQueryExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<int> result)
    {
        return base.NonQueryExecuting(command, eventData, result);
    }

    //Called just before EF intends to call ExecuteNonQueryAsync().
    public override ValueTask<InterceptionResult<int>> NonQueryExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        return base.NonQueryExecutingAsync(command, eventData, result, cancellationToken);
    }

    //Called immediately after EF calls ExecuteNonQuery().
    public override int NonQueryExecuted(DbCommand command, CommandExecutedEventData eventData, int result)
    {
        return base.NonQueryExecuted(command, eventData, result);
    }

    //Called immediately after EF calls ExecuteNonQueryAsync().
    public override ValueTask<int> NonQueryExecutedAsync(DbCommand command, CommandExecutedEventData eventData, int result, CancellationToken cancellationToken = default)
    {
        return base.NonQueryExecutedAsync(command, eventData, result, cancellationToken);
    }
    #endregion


}
