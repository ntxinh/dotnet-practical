using ASWA.Core.Entities;
using ASWA.Core.Interfaces;
using ASWA.Web.Interfaces;

namespace ASWA.Web.Services
{
    public class CustomerViewModelService: ICustomerViewModelService
    {
        private readonly IRepository<Customer> _customerRepository;

        public CustomerViewModelService(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }
    }
}