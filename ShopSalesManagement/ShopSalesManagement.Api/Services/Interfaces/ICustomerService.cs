﻿using ShopSalesManagement.Api.DTOs;

namespace ShopSalesManagement.Api.Services.Interfaces;

public interface ICustomerService
{
    IEnumerable<CustomerDto> GetAll();
    CustomerDto? GetById(int id);
    CustomerDto Create(CustomerCreateDto customerCreateDto);
    bool Update(int id, CustomerDto customerDto);
    bool Delete(int id);
}
