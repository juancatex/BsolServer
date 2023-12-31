﻿using Ardalis.Specification;

namespace TrackFinance.SharedKernel.Interfaces;
public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}
