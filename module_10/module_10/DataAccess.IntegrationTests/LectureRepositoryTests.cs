//using Microsoft.VisualBasic;
//using Microsoft.VisualStudio.TestPlatform.ObjectModel;
//using NUnit.Framework;
//using NUnit.Framework.Internal;
//using System.Net.NetworkInformation;
//using System.Reflection.Metadata;
//using System.Runtime.CompilerServices;
//using System.Runtime.ConstrainedExecution;
//using System.Security.Cryptography;
//using System;
//using Domain;
//using Microsoft.EntityFrameworkCore;
//using Moq;
//using AutoMapper;
//using System.Linq;
//using TestHelper;
//using System.Collections.Generic;

//namespace DataAccess.IntegrationTests
//{
//    public class LectureRepositoryTests
//    {
//        static MapperProfile myProfile = new MapperProfile();
//        static MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
//        Mapper _mapper = new Mapper(configuration);

//        private List<Lecture> _lectures = DataInitializer.GetAllLectures();
//        //private List<LectureDb> _lecturesDb = DataInitializer.GetAllDbLectures();

//        [SetUp]
//        public void Setup()
//        {
//        }

//        //[Test]
//        //public void Test1()
//        //{
//        //    var lecture1 = _lectures[0];
//        //    var lecture2 = _lectures[1];

//        //    var options = new DbContextOptionsBuilder<ApplicationDbContext>()
//        //        .UseInMemoryDatabase("lecture_db")
//        //        .Options;

//        //    var context = new ApplicationDbContext(options);
//        //    context.Database.EnsureCreated();

//        //    context.AddRange(_lecturesDb);
//        //    context.SaveChanges();
//        //    var newLects = context.Lectures.ToList();


//        //    using (context)
//        //    {
//        //        var repository = new LecturesRepository(context, _mapper);
//        //        //repository.Create(lecture1);
//        //        //repository.Create(lecture2);
//        //        var lects = repository.GetAll();

//        //        Assert.That(lects.Count, Is.EqualTo(6));
//        //    }


//            {
//                //public class CustomerRepositoryTests
//                //{
//                //[Fact]
//                //public void Save_Should_Save_The_Customer_And_Should_Return_All_Count_As_Two()
//                //{
//                //    var customer1 = new Domain.Customer("Caner Tosuner", "IST", DateTime.Today.AddYears(28));
//                //    var customer2 = new Domain.Customer("Caner Tosuner", "IST", DateTime.Today.AddYears(28));

//                //    var options = new DbContextOptionsBuilder<CustomerDbContext>()
//                //        .UseInMemoryDatabase("customer_db")
//                //        .Options;

//                //    using (var context = new CustomerDbContext(options))
//                //    {
//                //        var repository = new CustomerRepository(context);
//                //        repository.Save(customer1);
//                //        repository.Save(customer2);
//                //        context.SaveChanges();
//                //    }

//                //    using (var context = new CustomerDbContext(options))
//                //    {
//                //        var repository = new CustomerRepository(context);
//                //        repository.All().Count().Should().Be(2);
//                //    }
//                //}

//                //[Fact]
//                //public void Delete_Should_Delete_The_Customer_And_Should_Return_All_Count_As_One()
//                //{
//                //    var customer1 = new Domain.Customer("Caner Tosuner", "IST", DateTime.Today.AddYears(28));
//                //    var customer2 = new Domain.Customer("Caner Tosuner", "IST", DateTime.Today.AddYears(28));

//                //    var options = new DbContextOptionsBuilder<CustomerDbContext>()
//                //        .UseInMemoryDatabase("customer_db")
//                //        .Options;

//                //    using (var context = new CustomerDbContext(options))
//                //    {
//                //        var repository = new CustomerRepository(context);
//                //        repository.Save(customer1);
//                //        repository.Save(customer2);
//                //        context.SaveChanges();
//                //    }

//                //    using (var context = new CustomerDbContext(options))
//                //    {
//                //        var repository = new CustomerRepository(context);
//                //        repository.Delete(customer1.Id);
//                //        context.SaveChanges();
//                //    }

//                //    using (var context = new CustomerDbContext(options))
//                //    {
//                //        var repository = new CustomerRepository(context);
//                //        repository.All().Count().Should().Be(1);
//                //    }
//                //}

//                //[Fact]
//                //public void Update_Should_Update_The_Customer()
//                //{
//                //    var customer = new Domain.Customer("Caner Tosuner", "IST", DateTime.Today.AddYears(28));

//                //    var options = new DbContextOptionsBuilder<CustomerDbContext>()
//                //        .UseInMemoryDatabase("customer_db")
//                //        .Options;

//                //    using (var context = new CustomerDbContext(options))
//                //    {
//                //        var repository = new CustomerRepository(context);
//                //        repository.Save(customer);
//                //        context.SaveChanges();
//                //    }

//                //    customer.SetFields("Caner T", "IZM", customer.BirthDate);

//                //    using (var context = new CustomerDbContext(options))
//                //    {
//                //        var repository = new CustomerRepository(context);
//                //        repository.Update(customer);
//                //        context.SaveChanges();
//                //    }

//                //    using (var context = new CustomerDbContext(options))
//                //    {
//                //        var repository = new CustomerRepository(context);
//                //        var result = repository.Get(customer.Id);

//                //        result.Should().NotBe(null);
//                //        result.FullName.Should().Be(customer.FullName);
//                //        result.CityCode.Should().Be(customer.CityCode);
//                //        result.BirthDate.Should().Be(customer.BirthDate);
//                //    }
//                //}

//                //[Fact]
//                //public void Find_Should_Fid_The_Customer_And_Should_Return_All_Count_As_One()
//                //{
//                //    var customer1 = new Domain.Customer("Caner Tosuner", "IST", DateTime.Today.AddYears(28));
//                //    var customer2 = new Domain.Customer("Caner Tosuner", "IZM", DateTime.Today.AddYears(28));

//                //    var options = new DbContextOptionsBuilder<CustomerDbContext>()
//                //        .UseInMemoryDatabase("customer_db")
//                //        .Options;

//                //    using (var context = new CustomerDbContext(options))
//                //    {
//                //        var repository = new CustomerRepository(context);
//                //        repository.Save(customer1);
//                //        repository.Save(customer2);
//                //        context.SaveChanges();
//                //    }

//                //    using (var context = new CustomerDbContext(options))
//                //    {
//                //        var repository = new CustomerRepository(context);
//                //        var result = repository.Find(c => c.CityCode == customer1.CityCode);
//                //        result.Should().NotBeNull();
//                //        result.Count().Should().Be(1);
//                //    }
//                //}
//            }
//        }

//    }
//}