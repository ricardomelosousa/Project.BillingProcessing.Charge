﻿using Project.BillingProcessing.Charge.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BillingProcessing.Charge.Domain.Service.Interface
{
    public interface IChargeService
    {
        IEnumerable<ChargeEntity.Charge> FilterBy(Expression<Func<ChargeEntity.Charge, bool>> filterExpression);

        Task<ChargeEntity.Charge> FindOneAsync(Expression<Func<ChargeEntity.Charge, bool>> filterExpression);

        Task<ChargeEntity.Charge> FindByIdAsync(string id);

        Task InsertOneAsync(ChargeEntity.Charge document);
    }
}
