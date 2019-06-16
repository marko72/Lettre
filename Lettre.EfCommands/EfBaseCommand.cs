using Lettre.EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lettre.EfCommands
{
    public abstract class EfBaseCommand
    {
        protected LettreDbContext Context { get; }

        protected EfBaseCommand(LettreDbContext context)
        {
            Context = context;
        }
    }
}
