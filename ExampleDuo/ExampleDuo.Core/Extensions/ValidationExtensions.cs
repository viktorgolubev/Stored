﻿using ExampleDuo.Infrastructure.Models.Exceptions;

namespace ExampleDuo.Core.Extensions;

internal static class ValidationExtensions
{
    internal static void ThrowIfZeroOrLess(this int id, string type)
    {
        if (id < 1)
        {
            throw new ValidationException($"Id must be a positive number. Id: {id}, Type: {type}");
        }
    }
}