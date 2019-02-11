using CareerTrack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CareerTrack.Application.Users.Queries.GetUserDetail
{
    public class UserDetailModel
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Country { get; set; }
        public string Fax { get; set; }
        public string Phone { get; set; }
        public string PostalCode { get; set; }
        public string Region { get; set; }

        public static Expression<Func<User, UserDetailModel>> Projection
        {
            get
            {
                return customer => new UserDetailModel
                {
                    //Id = customer.CustomerId,
                    //Address = customer.Address,
                    //City = customer.City,
                    //CompanyName = customer.CompanyName,
                    //ContactName = customer.ContactName,
                    //ContactTitle = customer.ContactTitle,
                    //Country = customer.Country,
                    //Fax = customer.Fax,
                    //Phone = customer.Phone,
                    //PostalCode = customer.PostalCode,
                    //Region = customer.Region
                };
            }
        }

        public static UserDetailModel Create(User user)
        {
            return Projection.Compile().Invoke(user);
        }
    }
}
