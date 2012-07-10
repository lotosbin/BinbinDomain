using System;
namespace BinbinUnitOfWork
{
    public interface IIUnitOfWorkScope<TDataContext>
    {
        void JoinScope(TDataContext datacontext);
    }
}
