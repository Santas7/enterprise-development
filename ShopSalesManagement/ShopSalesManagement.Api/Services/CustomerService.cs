using ShopSalesManagement.Api.DTOs;
using ShopSalesManagement.Domain;

namespace ShopSalesManagement.Api.Services;

public class CustomerService : ICustomerService
{
    private readonly ApplicationDbContext _context;

    public CustomerService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<CustomerDTO> GetAll()
    {
        return _context.Customers
            .Select(c => new CustomerDTO
            {
                Id = c.Id,
                CardNumber = c.CardNumber,
                FullName = c.FullName
            })
            .ToList();
    }

    public CustomerDTO? GetById(int id)
    {
        var customer = _context.Customers.Find(id);
        return customer == null ? null : new CustomerDTO
        {
            Id = customer.Id,
            CardNumber = customer.CardNumber,
            FullName = customer.FullName
        };
    }

    public CustomerDTO Create(CustomerDTO customerDto)
    {
        var customer = new Customer
        {
            CardNumber = customerDto.CardNumber,
            FullName = customerDto.FullName
        };

        _context.Customers.Add(customer);
        _context.SaveChanges();

        customerDto.Id = customer.Id;
        return customerDto;
    }

    public bool Update(int id, CustomerDTO customerDto)
    {
        var customer = _context.Customers.Find(id);
        if (customer == null) return false;

        customer.CardNumber = customerDto.CardNumber;
        customer.FullName = customerDto.FullName;

        _context.SaveChanges();
        return true;
    }

    public bool Delete(int id)
    {
        var customer = _context.Customers.Find(id);
        if (customer == null) return false;

        _context.Customers.Remove(customer);
        _context.SaveChanges();
        return true;
    }
}
