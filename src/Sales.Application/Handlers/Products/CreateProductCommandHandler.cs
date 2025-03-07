﻿using AutoMapper;
using FluentValidation;
using MediatR;
using Sales.Application.Commands.Products;
using Sales.Application.DTOs;
using Sales.Application.Events;
using Sales.Application.Interfaces.MessageBrokers;
using Sales.Application.Interfaces.Repositories;
using Sales.Application.Shared;
using Sales.Domain.Entities;

namespace Sales.Application.Handlers.Products
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<ProductDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateProductCommand> _validator;
        private readonly IRabbitMQMessageSender _rabbitMQMessageSender;

        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper, IValidator<CreateProductCommand> validator, IRabbitMQMessageSender rabbitMQMessageSender)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _validator = validator;
            _rabbitMQMessageSender = rabbitMQMessageSender;
        }

        public async Task<Result<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var product = _mapper.Map<Product>(request);
            await _productRepository.AddAsync(product);
            await _rabbitMQMessageSender.SendMessage(new ProductCreatedEvent(product), QueuesNames.CreatedProductQueue);
            var productDto = _mapper.Map<ProductDto>(product);
            return Result<ProductDto>.Persisted(productDto, string.Format(Consts.EntityCreatedWithSuccess, nameof(Product)));
        }
    }
}