using NUnit.Framework;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Tradgardsgolf.Core.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore;
using Tradgardsgolf.Infrastructure.Entities;

namespace Tradgardsgolf.Tests.Repositories.CreateLoginRepository
{
    [TestFixture]
    public class EmailExists
    {

        [Test]
        public void ShouldReturnFalseWhenEmailDontExists()
        {
            var resolver = new Resolver(config =>
            {
                config.UseTradgardsgolfContext();
            });

            var repsoitory = resolver.Resolve<ICreateLoginRepository, Infrastructure.Repositories.CreateLoginRepository>();

            Assert.IsFalse(repsoitory.EmailExists("example@example.com"));
        }

        [Test]
        public void ShouldReturTrueWhenEmailAllreadyExists()
        {
            Player player = null;

            var resolver = new Resolver(config =>
            {
                config.UseEntity(new Player() { Email = "example@example.com" }, out player);
            });

            var repsoitory = resolver.Resolve<ICreateLoginRepository, Infrastructure.Repositories.CreateLoginRepository>();

            Assert.IsFalse(repsoitory.EmailExists(player.Email));
        }

        [Test]
        public void ShouldReturTrueWhenEmailAllreadyExistsWithDiffrentCase()
        {
            Player player = null;

            var resolver = new Resolver(config =>
            {
                config.UseEntity(new Player() { Email = "example@example.com" }, out player);
            });

            var repsoitory = resolver.Resolve<ICreateLoginRepository, Infrastructure.Repositories.CreateLoginRepository>();

            Assert.IsFalse(repsoitory.EmailExists(player.Email.ToUpper()));
        }


    }
}
