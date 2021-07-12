﻿using System;

namespace Catalog.Entities
{
    //Record immutable data type
    public record Item
    {
        //Items properties
        //Init is a only initializer set

        public Guid Id { get; init; }

        public string Name { get; init; }

        public decimal Price { get; init; }

        public DateTimeOffset CreateDate { get; init; }

    }
}