using System;
using System.Collections.Generic;
using System.Text;

namespace Lettre.Application.Interfaces
{
    public interface ICommand <TRequest>
    {
        void Execute(TRequest request);
    }
    public interface ICommand<TRequest, TResoult> /*where TRequest : class*/
    {
        TResoult Execute(TRequest request);
    }
    public interface ICommandTwoValues <TRequest, id>
    {
        void Execute(int id, TRequest request);
    }
}
