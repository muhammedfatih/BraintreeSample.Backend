using BraintreeSample.APIHelper.Converters;
using BraintreeSample.API.Models;
using BraintreeSample.API.Entities;
using System;
using System.Collections.Generic;
using BraintreeSample.API.RequestModels;

namespace BraintreeSample.API.Converters
{
    public class TransactionConverter : BaseConverter<TransactionEntity, TransactionModel>
    {
        public new TransactionEntity Convert(TransactionModel model)
        {
            return new TransactionEntity()
            {
                CreditCardId = model.CreditCardId,
                Amount = model.Amount,
                Guid = model.Guid.ToString(),
                IsActive = model.IsActive,
                IsDeleted = model.IsDeleted,
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt,
            };
        }
        public new TransactionModel Convert(TransactionEntity entity)
        {
            return new TransactionModel()
            {
                CreditCardId = entity.CreditCardId,
                Amount = entity.Amount,
                Guid = new Guid(entity.Guid),
                IsActive = entity.IsActive,
                IsDeleted = entity.IsDeleted,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
            };
        }
        public new List<TransactionModel> Convert(List<TransactionEntity> entities)
        {
            var models = new List<TransactionModel>();
            foreach (var entity in entities)
                models.Add(Convert(entity));
            return models;
        }
        public new List<TransactionEntity> Convert(List<TransactionModel> models)
        {
            var entities = new List<TransactionEntity>();
            foreach (var model in models)
                entities.Add(Convert(model));
            return entities;
        }
        public TransactionModel ConvertCreateModel(CreateTransactionModel model)
        {
            decimal amount;
            decimal.TryParse(model.Amount, out amount);
            return new TransactionModel()
            {
                Amount = amount
        };
    }
}
}