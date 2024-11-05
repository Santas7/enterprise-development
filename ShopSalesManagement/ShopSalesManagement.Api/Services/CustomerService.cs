using ShopSalesManagement.Api.DTOs;
using ShopSalesManagement.Api.Services.Interfaces;
using ShopSalesManagement.Domain;

namespace ShopSalesManagement.Api.Services;

public class CustomerService : ICustomerService
{
    private readonly ApplicationDbContext _context;

    public CustomerService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<CustomerDto> GetAll()
    {
        return _context.Customers
            .Select(c => new CustomerDto
            {
                Id = c.Id,
                CardNumber = c.CardNumber,
                FullName = c.FullName
            })
            .ToList();
    }

    public CustomerDto? GetById(int id)
    {
        var customer = _context.Customers.Find(id);
        return customer == null ? null : new CustomerDto
        {
            Id = customer.Id,
            CardNumber = customer.CardNumber,
            FullName = customer.FullName
        };
    }

    public CustomerDto Create(CustomerCreateDto customerCreateDto)
    {
        var customer = new Customer
        {
            CardNumber = customerCreateDto.CardNumber,
            FullName = customerCreateDto.FullName
        };

        _context.Customers.Add(customer);
        _context.SaveChanges();

        return new CustomerDto
        {
            Id = customer.Id,
            CardNumber = customer.CardNumber,
            FullName = customer.FullName
        };
    }

    public bool Update(int id, CustomerDto customerDto)
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
