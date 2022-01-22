﻿using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace CompanyEmployees.Extensions;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Company, CompanyDto>()
            .ForMember(c => c.FullAddress,
                opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));
        CreateMap<Employee, EmployeeDto>();
        CreateMap<CompanyForCreationDto, Company>();
        CreateMap<CompanyDto, Company>();
        CreateMap<EmployeeForCreationDto, Employee>();
        CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap();
        CreateMap<CompanyForUpdateDto, Company>();

    }
}