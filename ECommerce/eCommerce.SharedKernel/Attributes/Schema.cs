﻿using System;

namespace eCommerce.SharedKernel.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class Schema : Attribute
    {
        public string SchemaName { get; set; }

        public Schema(string schemaName)
        {
            SchemaName = schemaName;
        }
    }
}