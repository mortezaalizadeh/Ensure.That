﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using EnsureThat.Annotations;
using JetBrains.Annotations;

using NotNullAttribute = System.Diagnostics.CodeAnalysis.NotNullAttribute;

namespace EnsureThat.Enforcers
{
    /// <summary>
    /// Ensures for <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <remarks>MULTIPLE ENUMERATION OF PASSED ENUMERABLE IS POSSIBLE.</remarks>
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public sealed class EnumerableArg
    {
        [return: NotNull]
        [ContractAnnotation("value:null => halt")]
        public IEnumerable<T> HasItems<T>([ValidatedNotNull, InstantHandle]IEnumerable<T> value, [InvokerParameterName] string paramName = null, OptsFn optsFn = null)
        {
            Ensure.Any.IsNotNull(value, paramName, optsFn);

            if (!value.Any())
                throw Ensure.ExceptionFactory.ArgumentException(
                    ExceptionMessages.Collections_HasItemsFailed,
                    paramName,
                    optsFn);

            return value;
        }

        [return: NotNull]
        [ContractAnnotation("value:null => halt")]
        public IEnumerable<T> SizeIs<T>([ValidatedNotNull, InstantHandle]IEnumerable<T> value, int expected, [InvokerParameterName] string paramName = null, OptsFn optsFn = null)
        {
            Ensure.Any.IsNotNull(value, paramName, optsFn);

            var count = value.Count();

            if (count != expected)
                throw Ensure.ExceptionFactory.ArgumentException(
                    string.Format(ExceptionMessages.Collections_SizeIs_Failed, expected, count),
                    paramName,
                    optsFn);

            return value;
        }

        [return: NotNull]
        [ContractAnnotation("value:null => halt")]
        public IEnumerable<T> SizeIs<T>([ValidatedNotNull, InstantHandle]IEnumerable<T> value, long expected, [InvokerParameterName] string paramName = null, OptsFn optsFn = null)
        {
            Ensure.Any.IsNotNull(value, paramName, optsFn);

            var count = value.LongCount();

            if (count != expected)
                throw Ensure.ExceptionFactory.ArgumentException(
                    string.Format(ExceptionMessages.Collections_SizeIs_Failed, expected, count),
                    paramName,
                    optsFn);

            return value;
        }

        [return: NotNull]
        [ContractAnnotation("value:null => halt")]
        public IEnumerable<T> HasAny<T>([ValidatedNotNull, InstantHandle]IEnumerable<T> value, Func<T, bool> predicate, [InvokerParameterName] string paramName = null, OptsFn optsFn = null)
        {
            Ensure.Any.IsNotNull(value, paramName, optsFn);

            if (!value.Any(predicate))
                throw Ensure.ExceptionFactory.ArgumentException(
                    ExceptionMessages.Collections_Any_Failed,
                    paramName,
                    optsFn);

            return value;
        }
    }
}
