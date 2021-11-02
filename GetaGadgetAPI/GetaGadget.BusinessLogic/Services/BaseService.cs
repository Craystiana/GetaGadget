using GetaGadget.Domain.Interfaces;
using System;

namespace GetaGadget.BusinessLogic.Services
{
    public abstract class BaseService
    {
        private protected readonly IUnitOfWork UnitOfWork;

        protected BaseService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        private protected void Save()
        {
            try
            {
                UnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                // TODO: Log exception
                throw ex;
            }
        }
    }
}
