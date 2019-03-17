using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Monova.Entity;
using Stripe;

namespace Monova.Web.Controllers
{
    public class SubscriptionController : ApiController
    {
        [HttpGet("current")]
        public async Task<IActionResult> Current()
        {
            var subscription = await Db.Subscriptions.FirstOrDefaultAsync(x => x.UserId == UserId);
            if (subscription == null)
            {
                return Error("Subscription not found.", code: 404);
            }

            var featureList = await Db.SubscriptionFeatures
                .Include(x => x.SubscriptionTypeFeature)
                .Where(x => x.SubscriptionId == subscription.SubscriptionId)
                .OrderBy(x => x.SubscriptionTypeFeature.Sort)
                .ToListAsync();
            var features = new List<dynamic>();
            foreach (var feature in featureList)
            {
                features.Add(new
                {
                    feature.Name,
                    feature.Title,
                    feature.Description,
                    feature.Value,
                    feature.ValueUsed,
                    feature.ValueRemained
                });
            }

            var subscriptionType = await Db.SubscriptionTypes.FirstOrDefaultAsync(x => x.SubscriptionTypeId == subscription.SubscriptionTypeId);

            if (subscriptionType == null)
                return Error("There is no such subscription type.");

            return Success(data: new
            {
                title = subscriptionType.Title,
                id = subscription.SubscriptionId,
                typeId = subscription.SubscriptionTypeId,
                subscription.StartDate,
                subscription.EndDate,
                subscription.PaymentPeriod,
                PaymentPeriodText = subscription.PaymentPeriod.ToString(),
                features
            });
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Post([FromRoute]Guid id, [FromQuery]string token)
        {
            if (id != Guid.Empty)
            {
                var subscriptionType = await Db.SubscriptionTypes.FirstOrDefaultAsync(x => x.SubscriptionTypeId == id);
                if (subscriptionType == null)
                {
                    return Error("Subscription type not found", code: 404);
                }

                var subscription = await Db.Subscriptions.FirstOrDefaultAsync(x => x.UserId == UserId);

                if (subscription != null)
                {
                    if (subscription.SubscriptionTypeId == subscriptionType.SubscriptionTypeId)
                    {
                        return Error("You have already this subscription.");
                    }

                    return Success("We will add this feature.");
                }
                else
                {
                    subscription = new MVDSubscription
                    {
                        SubscriptionId = Guid.NewGuid(),
                        SubscriptionTypeId = subscriptionType.SubscriptionTypeId,
                        UserId = UserId,
                        StartDate = DateTime.UtcNow,
                        EndDate = subscriptionType.IsPaid ? DateTime.UtcNow.AddMonths(1) : DateTime.MinValue,
                        PaymentPeriod = MVDPaymentPeriodTypes.Monthly
                    };
                    await Db.AddAsync(subscription);

                    var features = await Db.SubscriptionTypeFeatures.Where(x => x.SubscriptionTypeId == subscriptionType.SubscriptionTypeId).ToListAsync();
                    foreach (var feature in features)
                    {
                        await Db.AddAsync(new MVDSubscriptionFeature
                        {
                            SubscriptionFeatureId = Guid.NewGuid(),
                            SubscriptionId = subscription.SubscriptionId,
                            SubscriptionTypeId = subscriptionType.SubscriptionTypeId,
                            SubscriptionTypeFeatureId = feature.SubscriptionTypeFeatureId,
                            Description = feature.Description,
                            Name = feature.Name,
                            Value = feature.Value,
                            Title = feature.Title,
                            ValueUsed = string.Empty,
                            ValueRemained = string.Empty
                        });
                    }
                }

                if (subscriptionType.Price > 0)
                {
                    try
                    {
                        var user = await Db.Users.FirstOrDefaultAsync(x => x.Id == UserId);
                        var customerService = new CustomerService();
                        var customerResult = await customerService.CreateAsync(new CustomerCreateOptions
                        {
                            Email = user.Email,
                            SourceToken = token
                        });

                        var items = new List<SubscriptionItemOption> {
                            new SubscriptionItemOption {PlanId = subscriptionType.Name}
                        };
                        var subscriptionService = new SubscriptionService();
                        var subscriptionOptions = new SubscriptionCreateOptions
                        {
                            CustomerId = customerResult.Id,
                            Items = items
                        };
                        var subscriptionResult = await subscriptionService.CreateAsync(subscriptionOptions);
                        if (subscriptionResult.Status == "active")
                        {
                            var payment = new MVDPayment
                            {
                                PaymentId = Guid.NewGuid(),
                                Provider = "stripe",
                                SubscriptionId = subscription.SubscriptionId,
                                UserId = UserId,
                                Token = subscriptionResult.LatestInvoiceId,
                                Amount = subscriptionType.Price,
                                Currency = "usd",
                                Date = DateTime.UtcNow,
                                Description = $"{subscriptionType.Title} {subscriptionType.Description}",
                            };
                            await Db.AddAsync(payment);
                        }
                        else
                        {
                            return Error("Payment not completed.", code: 400);
                        }
                    }
                    catch (Exception ex)
                    {
                        return Error("Payment error. Please check your credit card and details.", internalMessage: ex.Message, code: 400);
                    }
                }

                if (await Db.SaveChangesAsync() > 0)
                {
                    return Success("Your subscription has been updated.");
                }
                return Error("There is nothing to save.");
            }
            return Error("You must send subscription id.");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var subscriptionTypes = await Db.SubscriptionTypes.OrderBy(x => x.Price).ToListAsync();
            var list = new List<dynamic>();

            foreach (var subscription in subscriptionTypes)
            {
                var featureList = await Db.SubscriptionTypeFeatures
                    .Where(x => x.SubscriptionTypeId == subscription.SubscriptionTypeId)
                    .OrderBy(x => x.Sort)
                    .ToListAsync();

                var features = new List<dynamic>();
                foreach (var feature in featureList)
                {
                    features.Add(new
                    {
                        feature.Title,
                        feature.Description,
                        feature.IsFeature,
                        feature.Value
                    });
                }

                list.Add(new
                {
                    id = subscription.SubscriptionTypeId,
                    subscription.IsPaid,
                    subscription.Description,
                    subscription.Name,
                    subscription.Price,
                    subscription.Title,
                    features
                });
            }
            return Success(null, list);
        }
    }
}