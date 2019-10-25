using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;
using Vidly.Dtos;


namespace Vidly.App_Start
{
    public class MappingProfile : Profile //using AutoMapper;
    {
        public MappingProfile()
        {
            
            // Domain to Dto
            Mapper.CreateMap<Customer, CustomerDto>(); // generic method : creates mapping config from Tsrc to Tdst. When we call this method, automapper uses reflection to map props based on thier names
            Mapper.CreateMap<Movie, MovieDto>();
            Mapper.CreateMap<MembershipType, MembershipTypeDto>();
            Mapper.CreateMap<Genre, GenreDto>();
            
            // Dto to Domain (need to tell AutoMapper to ignore Id during mapping of a Dto to Domain and avoid exception when updating.)
            Mapper.CreateMap<CustomerDto, Customer>()
                 .ForMember(c => c.Id, opt => opt.Ignore());
 
            Mapper.CreateMap<MovieDto, Movie>()
                 .ForMember(c => c.Id, opt => opt.Ignore());
        }
    }
}