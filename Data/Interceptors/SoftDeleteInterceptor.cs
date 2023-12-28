using Core.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interceptors;

internal class SoftDeleteInterceptor : SaveChangesInterceptor
{
    public SoftDeleteInterceptor()
    {
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context == null) return result;

        var trackedEntries = eventData.Context.ChangeTracker.Entries()
            .Where(x => x.State == EntityState.Deleted && x.Entity is BaseFields);

        foreach (var entry in trackedEntries)
        {
            entry.State = EntityState.Modified;

            var entity = entry.Entity as BaseFields;
            entity.IsDelete = true;
        }

        return base.SavingChanges(eventData, result);
    }

    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        return base.SavedChanges(eventData, result);
    }


}
