﻿using System.Collections.Generic;
using System.Linq;
using Shouldly;
using Xunit;

namespace HybridDb.Tests.Bugs
{
    public class NotGeneratingNullableColumnsForMethodReturningValueTypes : HybridDbTests
    {
        [Fact]
        public void Fails()
        {
            var store = DocumentStore.ForTesting(TableMode.GlobalTempTables, x => x.UseConnectionString(connectionString));

            store.Configuration.Document<Entity>().With(x => x.SomeEnumerable.Count());

            var column = store.Configuration.GetDesignFor<Entity>().Table["SomeEnumerableCount"];
            column.Nullable.ShouldBe(true);
        }

        public new class Entity
        {
            public IEnumerable<object> SomeEnumerable { get; set; }
        }
    }
}