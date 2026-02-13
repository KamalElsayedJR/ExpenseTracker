using AutoMapper;
using BusinessLogicLayer.Dtos;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Expense, ExpenseDto>().ReverseMap();
        }
    }
}
