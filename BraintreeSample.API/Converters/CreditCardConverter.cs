using BraintreeSample.APIHelper.Converters;
using BraintreeSample.API.Entities;
using BraintreeSample.API.Models;
using BraintreeSample.API.RequestModels;
using System;
using System.Collections.Generic;

namespace BraintreeSample.API.Converters
{
    public class CreditCardConverter : BaseConverter<CreditCardEntity, CreditCardModel>
    {
        public new CreditCardEntity Convert(CreditCardModel model)
        {
            return new CreditCardEntity()
            {
                UserId = model.UserId,
                Token = model.Token,
                CardType = model.CardType,
                BinNumber = model.BinNumber,
                LastFour = model.LastFour,
                ExpirationYear = model.ExpirationYear,
                ExpirationMonth = model.ExpirationMonth,
                Guid = model.Guid.ToString(),
                IsActive = model.IsActive,
                IsDeleted = model.IsDeleted,
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt,
            };
        }
        public new CreditCardModel Convert(CreditCardEntity entity)
        {
            return new CreditCardModel()
            {
                UserId = entity.UserId,
                Token = entity.Token,
                CardType = entity.CardType,
                BinNumber = entity.BinNumber,
                LastFour = entity.LastFour,
                ExpirationYear = entity.ExpirationYear,
                ExpirationMonth = entity.ExpirationMonth,
                Guid = new Guid(entity.Guid),
                IsActive = entity.IsActive,
                IsDeleted = entity.IsDeleted,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
            };
        }
        public new List<CreditCardModel> Convert(List<CreditCardEntity> entities)
        {
            var models = new List<CreditCardModel>();
            foreach (var entity in entities)
                models.Add(Convert(entity));
            return models;
        }
        public new List<CreditCardEntity> Convert(List<CreditCardModel> models)
        {
            var entities = new List<CreditCardEntity>();
            foreach (var model in models)
                entities.Add(Convert(model));
            return entities;
        }
    }
}