﻿using System;
using System.Collections.Generic;
using HybridDb.Commands;
using Shouldly;
using Xunit;
using System.Linq;

namespace HybridDb.Tests.Performance
{
    public class QueryPerformanceTests : PerformanceTests, IUseFixture<QueryPerformanceTests.Fixture>
    {
        protected IDocumentStore store;

        [Fact]
        public void SimpleQueryWithoutMaterialization()
        {
            Time((out QueryStats stats) => store.Query(store.Configuration.GetDesignFor<LocalEntity>().Table, out stats))
                .DbTime.ShouldBeLessThan(3600);
        }

        [Fact]
        public void SimpleQueryWithMaterializationToDictionary()
        {
            Time((out QueryStats stats) => store.Query(store.Configuration.GetDesignFor<LocalEntity>().Table, out stats).ToList())
                .CodeTime.ShouldBeLessThan(160);
        }

        [Fact]
        public void SimpleQueryWithMaterializationToProjection()
        {
            Time((out QueryStats stats) => store.Query<LocalEntity>(store.Configuration.GetDesignFor<LocalEntity>().Table, out stats).ToList())
                .CodeTime.ShouldBeLessThan(16);
        }

        [Fact]
        public void QueryWithWindow()
        {
            Time((out QueryStats stats) => store.Query(store.Configuration.GetDesignFor<LocalEntity>().Table, out stats, skip: 200, take: 500))
                .DbTime.ShouldBeLessThan(160);
        }

        [Fact]
        public void QueryWithLateWindow()
        {
            Time((out QueryStats stats) => store.Query(store.Configuration.GetDesignFor<LocalEntity>().Table, out stats, skip: 9500, take: 500))
                .DbTime.ShouldBeLessThan(320);
        }

        [Fact]
        public void QueryWithWindowMaterializedToProjection()
        {
            Time((out QueryStats stats) => store.Query<LocalEntity>(store.Configuration.GetDesignFor<LocalEntity>().Table, out stats, skip: 200, take: 500).ToList())
                .TotalTime.ShouldBeLessThan(160);
        }

        public void SetFixture(Fixture data)
        {
            store = data.Store;
        }

        public class Fixture : HybridDbTests
        {
            public IDocumentStore Store => store;

            public Fixture()
            {
                store = (DocumentStore) Using(
                    DocumentStore.ForTesting(
                        TableMode.UseTempTables,
                        connectionString,
                        c => c.Document<LocalEntity>()
                            .With(x => x.SomeData)
                            .With(x => x.SomeNumber)));

                store.Initialize();

                var commands = new List<DatabaseCommand>();
                for (int i = 0; i < 10000; i++)
                {
                    commands.Add(new InsertCommand(
                        store.Configuration.GetDesignFor<LocalEntity>().Table,
                        Guid.NewGuid().ToString(),
                        new {SomeNumber = i, SomeData = "ABC"}));
                }

                store.Execute(commands.ToArray());
            }
        }
    }
}